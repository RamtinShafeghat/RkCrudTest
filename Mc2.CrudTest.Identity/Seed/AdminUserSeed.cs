using Mc2.CrudTest.Identity.Models;
using Microsoft.Extensions.Configuration;

namespace Mc2.CrudTest.Identity.Seed;

public class AdminUserSeed
{
    private readonly IConfiguration configuration;
    private readonly RayanKarIdentityDbContext dbContext;

    public AdminUserSeed(
        IConfiguration configuration,
        RayanKarIdentityDbContext dbContext)
    {
        this.configuration = configuration;
        this.dbContext = dbContext;
    }

    public async Task SeedAdminUser()
    {
        var role = "admin";

        var name = configuration.GetSection("Admin:User").Value.ToString();
        var email = configuration.GetSection("Admin:Email").Value.ToString();
        var pass = configuration.GetSection("Admin:Password").Value.ToString();

        var user = new ApplicationUser
        {
            UserName = name,
            NormalizedUserName = name.ToUpper(),
            Email = email,
            NormalizedEmail = email.ToUpper(),
            EmailConfirmed = true,
            LockoutEnabled = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        if (!dbContext.Roles.Any(r => r.Name == role))
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);
            await roleStore.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper() });
        }
        if (!dbContext.Users.Any(u => u.UserName == user.UserName))
        {
            user.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(user, pass);
            
            var userStore = new UserStore<ApplicationUser>(dbContext);
            await userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, role);
        }

        await dbContext.SaveChangesAsync();
    }
}