using System.ComponentModel.DataAnnotations.Schema;

namespace MyCarShop.Models.DomainModels
{
    public class Purchase
    {
        public int Id { get; set; }
        public int CarId { get; set; } // foreign key 
        public Car Car { get; set; } // Navigation Property
        public int? Quantity { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? Total { get; set; }
        public string UserId { get; set; } //Foreign key

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } // Navigation Property

    }
}
