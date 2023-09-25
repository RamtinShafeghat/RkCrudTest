namespace Mc2.CrudTest.SharedKernel.Contracts;

public interface IAggregateEventStore<T> where T : IAggregateRoot
{
    Task<ITransactionCenter> GetTransaction();
    Task<T> RehydreateAsync(string id);
    Task SaveAsync(T entity);
}
