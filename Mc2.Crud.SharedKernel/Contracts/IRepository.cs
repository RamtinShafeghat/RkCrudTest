namespace Mc2.Crud.SharedKernel.Contracts;

public interface IRepository<T> where T : IAggregateRoot
{
    Task<T> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
