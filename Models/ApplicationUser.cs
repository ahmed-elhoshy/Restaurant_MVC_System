using Microsoft.AspNetCore.Identity;

namespace Resturant.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Order>? Orders { get; set; }
    }
}
