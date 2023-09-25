using Mc2.CrudTest.Identity.Models;

namespace Mc2.CrudTest.Identity;

public class RayanKarIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public RayanKarIdentityDbContext()
    {

    }

    public RayanKarIdentityDbContext(DbContextOptions<RayanKarIdentityDbContext> options) : base(options)
    {

    }
}
