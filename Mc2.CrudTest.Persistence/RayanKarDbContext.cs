using Mc2.CrudTest.Core;
using Mc2.CrudTest.Core.CustomerAggregate;
using Mc2.CrudTest.Persistence.Converters;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Persistence;

public class RayanKarDbContext : DbContext
{
    public RayanKarDbContext(DbContextOptions<RayanKarDbContext> options)
        : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<EventData> EventDatas { get; set; }

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
