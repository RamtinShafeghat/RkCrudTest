using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Mc2.CrudTest.Persistence.EventStores;

public class EventStore : IEventStore
{
    private readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        NullValueHandling = NullValueHandling.Ignore
    };

    private readonly RayanKarDbContext dbContext;

    public EventStore(RayanKarDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task SaveEventsAsync(IReadOnlyCollection<IDomainEvent> events, int version,
        string aggregateId, string aggregateName = "Aggregate Name")
    {
        if (events.Count == 0) return;

        var eventsToSave = events.Select(e => new EventData
        {
            Id = Guid.NewGuid(),
            Name = e.GetType().Name,
            AggregateId = aggregateId,
            AggregateName = aggregateName,
            Data = JsonConvert.SerializeObject(e, Formatting.Indented, jsonSerializerSettings),
            Version = ++version,
            CreatedAt = e.CreatedAt
        });

        await this.dbContext.EventDatas.AddRangeAsync(eventsToSave);
    }

    public async Task<IReadOnlyCollection<IDomainEvent>> LoadEventsAsync(string aggregateId)
    {
        var query = await (from ed in this.dbContext.EventDatas
                     where ed.AggregateId == aggregateId
                     orderby ed.Version
                     select ed).ToListAsync(); 

        var domainEvents = query.Select(TransformEvent).Where(a => a != null).ToList().AsReadOnly();

        return domainEvents;
    }
    private IDomainEvent TransformEvent(EventData eventSelected) => 
        JsonConvert.DeserializeObject(eventSelected.Data, jsonSerializerSettings) as IDomainEvent;
}
