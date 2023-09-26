namespace Mc2.CrudTest.Persistence.Repositories;

public class BaseRepository<T> : IRepository<T> where T : class, IAggregateRoot
{
    protected readonly IRayanKarDbContext dbContext;

    public BaseRepository(IRayanKarDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        T t = await dbContext.Set<T>().FindAsync(id);
        return t;
    }
    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        await SaveChangesAsync();

        return entity;
    }
    public async Task UpdateAsync(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    protected virtual async Task SaveChangesAsync()
    {
        await dbContext.SaveAsync();
    }
}
