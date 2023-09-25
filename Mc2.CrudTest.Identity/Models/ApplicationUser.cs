using Microsoft.AspNetCore.Identity;

namespace Mc2.CrudTest.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } 
    public string LastName { get; set; }
}