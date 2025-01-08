using lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductContext _context;

        public AdminController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ProductListEditor()
        {
            return View();
        }

        // Метод для збереження нового продукту
        [HttpPost]
        public IActionResult ProductListEditor(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Katalog", "Shop"); // Повертаємося до списку продуктів після збереження
            }
            return View(product); // Повертаємо форму з помилками, якщо дані некоректні
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
