using EcommAlgebra.Data;
using EcommAlgebra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult Index(int? categoryId) // Make categoryId nullable
        {
            List<Product> products = _context.Products.ToList();

            if (categoryId.HasValue) // Check if categoryId has a value
            {
                products = (from product in _context.Products
                            join prodCat in _context.ProductCategory on product.Id equals prodCat.ProductId
                            where prodCat.CategoryId == categoryId.Value // Use Value property to get the integer value
                            select new Product
                            {
                                Id = product.Id,
                                Title = product.Title,
                                Description = product.Description,
                                Price = product.Price,
                                ImageName = product.ImageName
                            }).ToList();
            }

            ViewBag.Categories = _context.Category.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();

            return View(products);
        }


        public IActionResult ProductDetails(int id)
        {
            if (id == 0) return NotFound();

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}