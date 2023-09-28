using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.AcceptanceTests.Shared;

public class MockDatabaseContext : BaseDbContext, IRayanKarDbContext
{
    public MockDatabaseContext(DbContextOptions<MockDatabaseContext> options) : base(options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    public override bool InMemory => true;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "Mc2InMemory");
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Seed();
    }
}
