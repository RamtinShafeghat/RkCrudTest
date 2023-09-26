using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mc2.CrudTest.Persistence;

public interface IRayanKarDbContext
{
    public bool InMemory { get; }

    DbSet<Customer> Customers { get; set; }
    DbSet<EventData> EventDatas { get; set; }

    DbSet<T> Set<T>() where T : class;
    EntityEntry<T> Entry<T>(T item) where T : class;
    
    Task<int> SaveAsync();
    
    Task MigrateAsync();
    Task EnsureDeletedAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
}
