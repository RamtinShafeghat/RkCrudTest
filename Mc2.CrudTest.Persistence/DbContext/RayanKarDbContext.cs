using Mc2.CrudTest.Persistence.Converters;
using Microsoft.EntityFrameworkCore.Storage;

namespace Mc2.CrudTest.Persistence;

public class RayanKarDbContext : DbContext, IRayanKarDbContext
{
    public RayanKarDbContext(DbContextOptions<RayanKarDbContext> options)
        : base(options)
    {

    }

    public bool InMemory => false;

    public DbSet<Customer> Customers { get; set; }
    public DbSet<EventData> EventDatas { get; set; }

    public async Task<int> SaveAsync() => await this.SaveChangesAsync();
    
    public async Task MigrateAsync() => await this.Database.MigrateAsync();
    public async Task EnsureDeletedAsync() => await this.Database.EnsureDeletedAsync();
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await this.Database.BeginTransactionAsync();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(RayanKarDbContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);

        builder.Properties<DateOnly>()
               .HaveConversion<DateOnlyConverter>();
    }
}
