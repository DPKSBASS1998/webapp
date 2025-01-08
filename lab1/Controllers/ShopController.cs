using lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductContext _productContext;
        private readonly OrderContext _orderContext;
        private readonly ApplicationContext _userContext;

        public ShopController(ProductContext productContext, OrderContext orderContext, ApplicationContext userContext)
        {
            _productContext = productContext;
            _orderContext = orderContext;
            _userContext = userContext; 
        }

        public IActionResult Katalog()
        {
            var products = _productContext.Products.ToList(); // Отримуємо всі продукти з бази даних
            return View(products); // Передаємо список продуктів у вигляд
        }

        
        public IActionResult Info(int id)
        {
            var product = _productContext.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); 
            }

            return View(product); 
        }

        public IActionResult Korzina()
        {
            var orders = _orderContext.Orders.ToList();
            var products = _productContext.Products.ToList();

            var viewModel = new OrderWithProductsViewModel
            {
                Orders = orders,
                Products = products
            };

            return View(viewModel);
        }


        // Метод для оновлення статусу доставки
        [HttpPost]
        public IActionResult Deliver(int id)
        {
            var order = _orderContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null && !order.IsDelivered)
            {
                order.IsDelivered = true;
                _orderContext.SaveChanges();
            }

            return RedirectToAction("Korzina");
        }

        // Відображення списку користувачів для вибору
        [HttpGet] 
        public IActionResult SelectUser(int productId)
        {
            ViewBag.ProductId = productId; // Передаємо ID товару через ViewBag
            var users = _userContext.Users.ToList(); 
            return View(users); 
        }

        // Обробка додавання замовлення після вибору користувача
        [HttpPost]
        public IActionResult AddOrder(int userId, int productId)
        {
            // Перевірка наявності користувача
            var userExists = _userContext.Users.Any(u => u.Id == userId);
            if (!userExists)
            {
                return BadRequest("Некоректний ID користувача.");
            }

            // Перевірка наявності продукту
            var productExists = _productContext.Products.Any(p => p.Id == productId);
            if (!productExists)
            {
                return BadRequest("Некоректний ID продукту.");
            }

            // Пошук існуючого замовлення для цього користувача
            var existingOrder = _orderContext.Orders.FirstOrDefault(o => o.UserId == userId && !o.IsDelivered);

            if (existingOrder != null)
            {
                // Додати новий ID продукту до списку
                var productIds = existingOrder.ProductIds.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                if (!productIds.Contains(productId.ToString()))
                {
                    productIds.Add(productId.ToString());
                    existingOrder.ProductIds = string.Join(",", productIds);
                }
            }
            else
            {
                // Створення нового замовлення, якщо його не існує
                var newOrder = new Order
                {
                    UserId = userId,
                    ProductIds = productId.ToString(),
                    Status = "Не доставлено",
                    IsDelivered = false
                };

                _orderContext.Orders.Add(newOrder);
            }

            // Збереження змін
            _orderContext.SaveChanges();

            return RedirectToAction("Korzina");
        }

        [HttpPost]
        public IActionResult UpdateOrder(int orderId, int productId, string action)
        {
            // Отримуємо замовлення
            var order = _orderContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return RedirectToAction("Index");  // або будь-яка інша дія, якщо замовлення не знайдено
            }

            // Отримуємо список товарів з ProductIds
            var productIds = order.ProductIds.Split(',').ToList();

            // Виконання відповідної дії
            switch (action)
            {
                case "increase":
                    productIds.Add(productId.ToString());
                    break;

                case "decrease":
                    productIds.Remove(productId.ToString());
                    break;

                case "remove":
                    // Видалити тільки одну одиницю цього товару
                    productIds.Remove(productId.ToString());
                    break;

                default:
                    break;
            }

            // Оновлення ProductIds в замовленні
            order.ProductIds = string.Join(",", productIds);

            // Збереження змін
            _orderContext.SaveChanges();

            // Перенаправлення назад на сторінку кошика
            return RedirectToAction("Korzina");
        }

        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            var order = _orderContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                // Видалення замовлення
                _orderContext.Orders.Remove(order);
                _orderContext.SaveChanges();
            }

            return RedirectToAction("Korzina"); 
        }

    }
}
