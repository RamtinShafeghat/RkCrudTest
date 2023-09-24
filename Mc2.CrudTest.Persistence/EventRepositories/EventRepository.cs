//using Mc2.Crud.SharedKernel;
//using Mc2.Crud.SharedKernel.Contracts;
//using Mc2.CrudTest.Application.Contracts.Persistence;

//namespace Mc2.CrudTest.Persistence.EventRepositories;

//internal class EventRepository<T, TKey> : IEventRepository<T, TKey> where T : AggregateRoot<TKey>, new()
//{
//    private readonly IEventStore<T, TKey> eventStore;
//    public EventRepository(IEventStore<T, TKey> eventStore)
//    {
//        this.eventStore = eventStore;
//    }

//    public async Task<T> GetAsync(TKey aggregateId)
//    {
//        var events = await this.eventStore.LoadAsync(aggregateId);

//        return events.Count > 0 ? new T(events) : null;
//    }

//    public async Task<TKey> SaveAsync(T aggregate)
//    {
//        await this.eventStore.SaveAsync(aggregate.Id, aggregate.Version, aggregate.DomainEvents);
//        return aggregate.Id;
//    }
//}
