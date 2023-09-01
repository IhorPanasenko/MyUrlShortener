using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<UrlLink> Links { get; set; }
    }
}
