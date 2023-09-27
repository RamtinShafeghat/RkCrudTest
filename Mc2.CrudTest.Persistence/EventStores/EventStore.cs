using Newtonsoft.Json;

namespace Mc2.CrudTest.Persistence.EventStores;

public class EventStore : IEventStore
{
    public static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        NullValueHandling = NullValueHandling.Ignore
    };

    private readonly IRayanKarDbContext dbContext;

    public EventStore(IRayanKarDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ITransactionCenter> GetTransaction()
    {
        if (this.dbContext.InMemory)
            return new TransactionCenter();

        var transaction = await this.dbContext.BeginTransactionAsync();
        return new TransactionCenter(transaction);
    }

    public async Task SaveEventsAsync(IReadOnlyCollection<IDomainEvent> events, int version,
        string aggregateId, string aggregateName = "Aggregate Name")
    {
        if (events.Count == 0) return;
        var eventsToSave = GetEventData(events, version, aggregateId, aggregateName);

        await this.dbContext.EventDatas.AddRangeAsync(eventsToSave);
        await this.dbContext.SaveAsync();
    }
    public static IEnumerable<EventData> GetEventData(IReadOnlyCollection<IDomainEvent> events, 
        int version, string aggregateId, string aggregateName)
    {
        return events.Select(e => new EventData
        {
            Id = Guid.NewGuid(),
            Name = e.GetType().Name,
            AggregateId = aggregateId,
            AggregateName = aggregateName,
            Data = JsonConvert.SerializeObject(e, Formatting.Indented, jsonSerializerSettings),
            Version = ++version,
            CreatedAt = e.CreatedAt
        });
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
