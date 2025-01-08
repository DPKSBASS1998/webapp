using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ProductIds { get; set; } = ""; // Зберігає ID продуктів через кому
        public bool IsDelivered { get; set; } = false; // Статус доставки
        public string Status { get; set; } = "Не доставлено"; // Статус
    }

}
