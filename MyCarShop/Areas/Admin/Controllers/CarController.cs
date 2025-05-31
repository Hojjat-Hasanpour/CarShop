using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCarShop.Models.Context;
using MyCarShop.Models.DomainModels;
using MyCarShop.Models.ViewModels;

namespace MyCarShop.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Developer")]
    [Area("Admin")]
    public class CarController(ShopContext context, IWebHostEnvironment webHostEnvironment) : Controller
    {
        private readonly ShopContext _context = context;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [Route("{area}/Cars/{brand?}")]
        public async Task<IActionResult> List(string brand = "all")
        {
            IEnumerable<Car> cars = brand.Equals("all")
                 ? await _context.Cars.ToArrayAsync()
                 : await _context.Cars.Where(c => c.Brand == brand).ToArrayAsync();
            var carTypes = await _context.Cars.Select(c => c.Brand).Distinct().ToArrayAsync();
            //ViewBag.CarTypes = carTypes;
            var carsList = new CarsListViewModel
            {
                Cars = cars,
                CarTypes = carTypes
            };
            return View(carsList);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            if (id.HasValue)
            {
                var car = await _context.Cars.FindAsync(id);
                if (car != null)
                    return View(car);
            }
            return View(new Car());
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Car car, IFormFile? imageUpload)
        {
            if (!ModelState.IsValid)
                return View(car);

            if (imageUpload != null)
            {
                if (car.Id == 0)
                {
                    car.ImageFileName = Guid.NewGuid() + "_" + imageUpload.FileName;
                }
                else
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid() + "_" + imageUpload.FileName;
                    if (car.ImageFileName != "TempCar.jpg")
                    {
                        string existingFilePath = Path.Combine(uploadsFolder, car.ImageFileName!);
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }
                    }
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    await using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageUpload.CopyToAsync(fileStream);
                    }
                    car.ImageFileName = uniqueFileName;
                }
            }
            else
            {
                if (car.Id == 0)
                    car.ImageFileName = "TempCar.jpg";
            }

            if (car.Id == 0)
                await _context.Cars.AddAsync(car);
            else
                _context.Update(car);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List), "Car", new { area = "Admin" });
        }

        //[HttpGet]
        //public IActionResult Delete(int id)
        //{

        //}


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car is null)
                return NotFound();
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}
