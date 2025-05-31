using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCarShop.Models.Context;
using MyCarShop.Models.DomainModels;
using MyCarShop.Models.ViewModels;

namespace MyCarShop.Controllers
{
    public class CarController : Controller
    {
        private readonly ShopContext _context;
        public CarController(ShopContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Route("Cars/{brand?}")]
        public async Task<IActionResult> List(string brand = "all")
        {
            IEnumerable<Car> cars = brand.Equals("all")
                ? await _context.Cars.ToArrayAsync()
                : await _context.Cars.Where(c => c.Brand == brand).ToArrayAsync();
            var carTypes = await _context.Cars.Select(c => c.Brand).Distinct().ToArrayAsync();
            var carsList = new CarsListViewModel
            {
                Cars = cars,
                CarTypes = carTypes
            };
            return View(carsList);
        }

        [Route("car/{id:int}/{slug?}")]
        public async Task<IActionResult> Details(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car is null)
                return NotFound();

            return View(car);
        }
    }
}
