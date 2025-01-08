using lab1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace lab1.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult PersonInfo(string name, int age)
        {
            ViewBag.Name = name;
            ViewBag.Age = age;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Lobby()
        {
            return View();
        }
        public IActionResult Submit()
        {
            return View();
        }


        [HttpGet]
        public IActionResult lab3() {
            return View();
        }

    }


}
