using Microsoft.AspNetCore.Identity;

namespace WebApplication7.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}