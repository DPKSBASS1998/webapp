using lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Threading.Tasks;

namespace lab1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string Username, string Email, string FullName, string Password)
        {

            var newUser = new User
            {
                Username = Username,
                Email = Email,
                FullName = FullName,
                Password = Password
            };


            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            Debug.WriteLine("Записано: " + newUser.Username);


            return RedirectToAction("ShowData");
        }

        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Користувач успішно видалений.";
            }

            return RedirectToAction("ShowData");
        }

        public IActionResult ShowData(int? userId)
        {
            var usersList = _context.Users.ToList();

            User selectedUser = null;
            if (userId.HasValue)
            {
                selectedUser = _context.Users.Find(userId.Value);
            }

            ViewBag.SelectedUser = selectedUser;
            return View(usersList);
        }
        public IActionResult EditUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user); 
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User user)
        {

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.FullName = user.FullName;

            
            _context.Users.Update(existingUser);

            
            await _context.SaveChangesAsync();

            
            return RedirectToAction("ShowData");
        }

    }
}
