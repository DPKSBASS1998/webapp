using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Product
    {
        public int Id { get; set; } // Унікальний ідентифікатор продукту

        [Required]
        public string Name { get; set; } // Назва продукту

        [Required]
        public string Description { get; set; } // Опис продукту

        [Required]
        public string ImageUrl { get; set; } // Посилання на зображення продукту

        public decimal Price { get; set; } // Ціна продукту (за бажанням)
    }
}
