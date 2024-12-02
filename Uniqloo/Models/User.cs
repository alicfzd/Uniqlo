using Microsoft.AspNetCore.Identity;

namespace Uniqloo.Models
{
    public class User : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
        public string Fullname { get; set; }
        public string? Address { get; set; }
    }
}
