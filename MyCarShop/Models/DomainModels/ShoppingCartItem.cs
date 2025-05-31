namespace MyCarShop.Models.DomainModels
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public int Quantity { get; set; }
    }
}
