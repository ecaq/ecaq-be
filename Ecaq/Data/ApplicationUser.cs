using Microsoft.AspNetCore.Identity;

namespace Ecaq.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
    }

}
