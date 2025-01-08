using Microsoft.EntityFrameworkCore;

namespace lab1.Models
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<Order> Orders { get; set; } // Таблиця замовлень
        public DbSet<User> Users { get; set; }   // Таблиця користувачів

        
    }
}
