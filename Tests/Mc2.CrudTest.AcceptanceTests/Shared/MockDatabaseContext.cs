using Mc2.CrudTest.Persistence.Configurations;
using Mc2.CrudTest.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mc2.CrudTest.AcceptanceTests.Shared;

public class MockDatabaseContext : DbContext, IRayanKarDbContext
{
    public MockDatabaseContext(DbContextOptions options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public bool InMemory => true;

    public DbSet<Customer> Customers { get; set; }
    public DbSet<EventData> EventDatas { get; set; }

    public async Task<int> SaveAsync() => await this.SaveChangesAsync();

    public async Task MigrateAsync() => await this.Database.MigrateAsync();
    public async Task EnsureDeletedAsync() => await this.Database.EnsureDeletedAsync();
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await this.Database.BeginTransactionAsync();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "Mc2InMemory");
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        new CustomerConfiguration().Configure(builder.Entity<Customer>());
        new EventDataConfiguration().Configure(builder.Entity<EventData>());
        
        builder.Seed();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);

        builder.Properties<DateOnly>()
               .HaveConversion<DateOnlyConverter>();
    }
}
