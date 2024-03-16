using Microsoft.AspNetCore.Identity;

namespace Ecaq.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUserWithRole
{
    public ApplicationUser? AppUser { get; set; }
    public string Role { get; set; }
    public List<IdentityRole> Roles { get; set; }
}
