using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyCarShop.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CarType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "CarType", "Color", "ImageFileName", "Model", "Price", "Year" },
                values: new object[,]
                {
                    { 1, "Audi", "Sedan", "Black", "CarDB_6.jpg", "A6", 22000.0m, 2022 },
                    { 2, "Mercedes Benz", "Sedan", "White", "CarDB_1.jpg", "MG 1", 95000.0m, 2023 },
                    { 3, "BMW", "Coupe", "Red", "CarDB_2.jpg", "M3", 67000.0m, 2025 },
                    { 4, "Aston Martin", "Coupe", "Blue", "CarDB_10.jpg", "DB 12", 55000.0m, 2023 },
                    { 5, "Ford", "Pickup", "White", "CarDB_5.jpg", "Ranger", 34000.0m, 2024 },
                    { 6, "Ferrari", "Sport Car", "Red", "CarDB_4.jpg", "458 Italia", 99000.0m, 2025 },
                    { 7, "BMW", "Sedan", "Black", "CarDB_7.jpg", "M6", 80000.0m, 2025 },
                    { 8, "Lamborghini", "Sport Car", "Green", "CarDB_3.jpg", "Gallard", 70000.0m, 2020 },
                    { 9, "Mercedes Benz", "Coupe", "White", "CarDB_8.jpg", "SLX", 45000.0m, 2023 },
                    { 10, "BMW", "Sedan", "White", "CarDB_9.jpg", "M5", 44000.0m, 2024 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
