using EcommAlgebra.Data;
using EcommAlgebra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EcommAlgebra.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;   
        }
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {

            if (ModelState.IsValid)
            {
                //pronaći lokaciju gdje ću spremitit sliku
                string wwwRootPath = "wwwroot";
                // ime fajla
                string fileName = product.ImageFile.FileName;
                product.ImageName = fileName;
                string path = wwwRootPath + "/Images/" + fileName;
                // spremiti na file system

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    product.ImageFile.CopyTo(fileStream);
                }

                // spremiti u bazu
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

    }
}
