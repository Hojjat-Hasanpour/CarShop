using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyCarShop.Models.DomainModels;

namespace MyCarShop.Models.Context
{
    public class ShopContext : IdentityDbContext<ApplicationUser>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        { //Readable Code
        }


        public DbSet<Car> Cars { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>().HasData(
                new Car { Id = 1, Brand = "Audi", Model = "A6", Year = 2022, Color = "Black", CarType = "Sedan", Price = 22000.0m, ImageFileName = "CarDB_6.jpg" },
                new Car { Id = 2, Brand = "Mercedes Benz", Model = "MG 1", Year = 2023, Color = "White", CarType = "Sedan", Price = 95000.0m, ImageFileName = "CarDB_1.jpg" },
                new Car { Id = 3, Brand = "BMW", Model = "M3", Year = 2025, Color = "Red", CarType = "Coupe", Price = 67000.0m, ImageFileName = "CarDB_2.jpg" },
                new Car { Id = 4, Brand = "Aston Martin", Model = "DB 12", Year = 2023, Color = "Blue", CarType = "Coupe", Price = 55000.0m, ImageFileName = "CarDB_10.jpg" },
                new Car { Id = 5, Brand = "Ford", Model = "Ranger", Year = 2024, Color = "White", CarType = "Pickup", Price = 34000.0m, ImageFileName = "CarDB_5.jpg" },
                new Car { Id = 6, Brand = "Ferrari", Model = "458 Italia", Year = 2025, Color = "Red", CarType = "Sport Car", Price = 99000.0m, ImageFileName = "CarDB_4.jpg" },
                new Car { Id = 7, Brand = "BMW", Model = "M6", Year = 2025, Color = "Black", CarType = "Sedan", Price = 80000.0m, ImageFileName = "CarDB_7.jpg" },
                new Car { Id = 8, Brand = "Lamborghini", Model = "Gallard", Year = 2020, Color = "Green", CarType = "Sport Car", Price = 70000.0m, ImageFileName = "CarDB_3.jpg" },
                new Car { Id = 9, Brand = "Mercedes Benz", Model = "SLX", Year = 2023, Color = "White", CarType = "Coupe", Price = 45000.0m, ImageFileName = "CarDB_8.jpg" },
                new Car { Id = 10, Brand = "BMW", Model = "M5", Year = 2024, Color = "White", CarType = "Sedan", Price = 44000.0m, ImageFileName = "CarDB_9.jpg" }
            );
        }
    }
}
