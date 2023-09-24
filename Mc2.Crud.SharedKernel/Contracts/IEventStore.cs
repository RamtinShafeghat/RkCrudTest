namespace Mc2.Crud.SharedKernel.Contracts;

public interface IEventStore 
{
    Task SaveEventsAsync(IReadOnlyCollection<IDomainEvent> events, 
        int version, string aggregateId, string aggregateName = "AggregateName");

    Task<IReadOnlyCollection<IDomainEvent>> LoadEventsAsync(string aggregateId);
}
