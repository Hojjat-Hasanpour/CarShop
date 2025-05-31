using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCarShop.Models.Context;
using MyCarShop.Models.DomainModels;
using MyCarShop.Models.Extensions;
using MyCarShop.Models.ViewModels;
using System.Security.Claims;

namespace MyCarShop.Controllers
{
    [Authorize]
    public class ShoppingCartController(ShopContext context) : Controller
    {
        private readonly ShopContext _context = context;
        //private readonly UserManager<ApplicationUser> _userManager = userManager;
        //private List<ShoppingCartItem> _cartItems = new();
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> AddToCart(int id) //View Result
        {
            var carToAdd = await _context.Cars.FindAsync(id);
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new();
            if (carToAdd == null)
                return NotFound();
            var existingCartItem = cartItems.FirstOrDefault(item => item.Car.Id == id);
            if (existingCartItem == null)
            {
                cartItems.Add(new ShoppingCartItem
                {
                    Car = carToAdd,
                    Quantity = 1
                });
            }
            else
            {
                existingCartItem.Quantity++;
            }
            HttpContext.Session.Set("Cart", cartItems);
            TempData["CartMessage"] = $"{carToAdd.Brand} {carToAdd.Model} added to Cart.";
            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new();
            var cartViewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                TotalPrice = cartItems.Sum(item => item.Car.Price * item.Quantity)
            };
            ViewBag.CartMessage = TempData["CartMessage"];
            return View(cartViewModel);
        }

        public IActionResult RemoveItem(int id)
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new();
            var itemToRemove = cartItems.FirstOrDefault(item => item.Car.Id == id);
            if (itemToRemove == null)
                return NotFound();

            if (itemToRemove.Quantity > 1)
                itemToRemove.Quantity--;
            else
                cartItems.Remove(itemToRemove);

            HttpContext.Session.Set("Cart", cartItems);
            TempData["CartMessage"] = $"{itemToRemove.Car.Brand} {itemToRemove.Car.Model} Removed from Cart.";
            return RedirectToAction("ViewCart");
            // ToDo Subtract message for shopping cart
        }

        public IActionResult RemoveAllItems()
        {
            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseItems()
        {
            var cartItems = HttpContext.Session.Get<List<ShoppingCartItem>>("Cart") ?? new();
            //var user = await _userManager.FindByNameAsync(User.Identity!.Name!);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            foreach (var item in cartItems)
            {
                // Save each item as a purchase
                await _context.Purchases.AddAsync(new Purchase
                {
                    CarId = item.Car.Id,
                    Quantity = item.Quantity,
                    PurchaseDate = DateTime.Now,
                    Total = item.Car.Price * item.Quantity,
                    UserId = userId
                });
            }
            // Save Changes to database
            await _context.SaveChangesAsync();
            // Clear the cart
            HttpContext.Session.Set("Cart", new List<ShoppingCartItem>());
            TempData["PurchaseMessage"] = "Purchase completed successfully.";
            return RedirectToAction("ShowUserInvoices");
        }

        public async Task<IActionResult> ShowUserInvoices()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var allUserInvoices = await _context.Purchases.Where(p => p.UserId == userId).Include(p => p.Car).ToArrayAsync();
            ViewBag.PurchaseMessage = TempData["PurchaseMessage"] ?? null;
            return View(allUserInvoices);
        }
    }
}
