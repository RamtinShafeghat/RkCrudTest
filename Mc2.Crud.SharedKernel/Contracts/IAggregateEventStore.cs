namespace Mc2.Crud.SharedKernel.Contracts;

public interface IAggregateEventStore<T> where T : IAggregateRoot
{
    Task<T> RehydreateAsync(string id);
    Task SaveAsync(T entity);
}
