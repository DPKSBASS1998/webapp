namespace lab1.Models
{
    public class OrderWithProductsViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
