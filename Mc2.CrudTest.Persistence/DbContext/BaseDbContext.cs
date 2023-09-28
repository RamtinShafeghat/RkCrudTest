using Mc2.CrudTest.Persistence.Configurations;
using Mc2.CrudTest.Persistence.Converters;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mc2.CrudTest.Persistence;

public abstract class BaseDbContext : DbContext, IRayanKarDbContext
{
    public BaseDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public abstract bool InMemory { get; }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<EventData> EventDatas { get; set; }

    public async Task<int> SaveAsync() => await this.SaveChangesAsync();

    public async Task MigrateAsync() => await this.Database.MigrateAsync();
    public async Task EnsureDeletedAsync() => await this.Database.EnsureDeletedAsync();
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await this.Database.BeginTransactionAsync();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new CustomerConfiguration().Configure(builder.Entity<Customer>());
        new EventDataConfiguration().Configure(builder.Entity<EventData>());
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);

        builder.Properties<DateOnly>()
               .HaveConversion<DateOnlyConverter>();
    }
}
