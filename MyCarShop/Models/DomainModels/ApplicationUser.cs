using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCarShop.Models.DomainModels
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Purchase>? Purchases { get; set; }

        [NotMapped] public IList<string> RoleNames { get; set; } = null!;
    }
}
