using System.ComponentModel.DataAnnotations;

namespace MyCarShop.Models.DomainModels
{
    public class Car
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [MaxLength(200, ErrorMessage = "The maximum length of brand name is 200 characters")]
        public string? Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [MaxLength(200, ErrorMessage = "The maximum length of model name is 200 characters")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(2000, 2025, ErrorMessage = "Year must be between 2000 and 2025")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [MaxLength(100, ErrorMessage = "The maximum length of type name is 100 characters")]
        public string? CarType { get; set; } // Sedan, Hatchback, Convertible, Coupe, Electric, Hybrid and so on

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 100000, ErrorMessage = "Price must be between 0 and 100000")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Color is required")]
        [MaxLength(50, ErrorMessage = "The maximum length of color name is 50 characters")]
        public string? Color { get; set; }

        [MaxLength(300, ErrorMessage = "The maximum length of image file name is 300 characters")]
        public string? ImageFileName { get; set; }

        //Slug read only property
        public string Slug => $"{Brand}-{Model}-{Year}".ToLower().Replace(" ", "-");

    }
}
