using Microsoft.AspNetCore.Identity;
using MyCarShop.Models.DomainModels;

namespace MyCarShop.Models.ViewModels
{
    public class UserViewModel
    {
        public IEnumerable<ApplicationUser> Users { get; set; } = null!;
        public IEnumerable<IdentityRole> Roles { get; set; } = null!;
    }
}
