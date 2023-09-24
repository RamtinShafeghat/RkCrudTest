using Mc2.Crud.SharedKernel.Contracts;

namespace Mc2.CrudTest.Application.Contracts.Persistence;

public interface IEventStore<T, TKey> where T : class, IAggregateRoot
{
    Task SaveAsync(T aggregate,
                   int originatingVersion,
                   IReadOnlyCollection<IDomainEvent> events,
                   string aggregateName = "AggregateName");

    Task<IReadOnlyCollection<IDomainEvent>> LoadAsync(TKey aggregateId);
}
