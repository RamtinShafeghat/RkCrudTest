namespace Mc2.Crud.SharedKernel.Contracts;

public interface IEventRepository<T, TKey> where T : class, IAggregateRoot
{
    Task<TKey> SaveAsync(T aggregate);
    Task<T> GetAsync(TKey aggregateId);
}
