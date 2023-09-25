namespace Mc2.CrudTest.SharedKernel.Contracts;

public interface IEventStore
{
    Task<ITransactionCenter> GetTransaction();

    Task SaveEventsAsync(IReadOnlyCollection<IDomainEvent> events, 
        int version, string aggregateId, string aggregateName = "AggregateName");

    Task<IReadOnlyCollection<IDomainEvent>> LoadEventsAsync(string aggregateId);
}
