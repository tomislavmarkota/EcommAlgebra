using EcommAlgebra.Data;
using EcommAlgebra.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommAlgebra.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;  

        public HomeController(ApplicationDbContext context)
        {
            _context = context; 
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}