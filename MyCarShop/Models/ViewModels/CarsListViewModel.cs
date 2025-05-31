using MyCarShop.Models.DomainModels;

namespace MyCarShop.Models.ViewModels
{
    public class CarsListViewModel
    {
        public required IEnumerable<Car> Cars { get; set; }
        public required IEnumerable<string?> CarTypes { get; set; }
    }
}
