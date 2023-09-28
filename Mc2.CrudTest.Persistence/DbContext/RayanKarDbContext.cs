namespace Mc2.CrudTest.Persistence;

public class RayanKarDbContext : BaseDbContext, IRayanKarDbContext
{
    public RayanKarDbContext(DbContextOptions<RayanKarDbContext> options)
        : base(options)
    {

    }
    public override bool InMemory => false;
}
