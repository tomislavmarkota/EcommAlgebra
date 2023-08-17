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

        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
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
                
            
                if (product.ImageFile != null)
                {
                    
                    string wwwRootPath = "wwwroot";
                    string fileName = Path.GetFileName(product.ImageFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName; // Add a unique identifier

                    product.ImageName = uniqueFileName;
                    string path = Path.Combine(wwwRootPath, "Images", uniqueFileName);

                    // Save the file to the unique path
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        product.ImageFile.CopyTo(fileStream);
                    }
                }

                // spremiti u bazu
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null) return NotFound();


            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (product == null || product.Id == 0)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = "wwwroot";

                if (product.ImageFile != null)
                {
                    string fileName = Path.GetFileName(product.ImageFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName; // Add a unique identifier

                    product.ImageName = uniqueFileName;
                    string path = Path.Combine(wwwRootPath, "Images", uniqueFileName);

                    // Save the file to the unique path
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        product.ImageFile.CopyTo(fileStream);
                    }
                }

                _context.Update(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null) return NotFound();

            _context.Remove(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }



    }
}
