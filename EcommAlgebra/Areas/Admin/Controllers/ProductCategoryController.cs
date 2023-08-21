using EcommAlgebra.Data;
using EcommAlgebra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace EcommAlgebra.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin, Editor")]
    public class ProductCategoryController : Controller
        {
            private ApplicationDbContext _dbContext;

            public ProductCategoryController(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public IActionResult Index(int productId)
            {
                ViewBag.ProductId = productId;

                var productCategoryList = _dbContext.ProductCategory
                    .Where(pc => pc.ProductId == productId)
                    .Select(pc => new ProductCategory()
                    {
                        Id = pc.Id,
                        ProductId = pc.ProductId,
                        CategoryId = pc.CategoryId,
                        ProductTitle = _dbContext
                        .Products
                        .SingleOrDefault(x => x.Id == pc.ProductId).Title,
                        CategoryTitle = _dbContext
                        .Category
                        .SingleOrDefault(x => x.Id == pc.CategoryId).Title,
                    });

                return View(productCategoryList);
            }


            public IActionResult Create(int productId)
            {
                ViewBag.ProductId = productId;

                // ovaj dio koji će nam pomoći povezivanje prozivoda s kategorijom
                // kategoriju ćemo staviti u padajući izbornik
                ViewBag.Categories = _dbContext.Category
                    .Select
                    (
                        c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.Title,
                        }
                    ).ToList();


                return View();
            }

            [HttpPost]
            public IActionResult Create(ProductCategory productCategory)
            {
                if (ModelState.IsValid)
                {
                    _dbContext.ProductCategory.Add(productCategory);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
                }

                return View(productCategory);
            }

            public IActionResult Details(int id)
            {
                if (id == 0) return NotFound();

                var productCategory = _dbContext.ProductCategory
                    .SingleOrDefault(pc => pc.Id == id);

                productCategory.ProductTitle = _dbContext
                        .Products
                        .SingleOrDefault(x => x.Id == productCategory.ProductId).Title;
                productCategory.CategoryTitle = _dbContext
                 .Category
                 .SingleOrDefault(x => x.Id == productCategory.CategoryId).Title;


                if (productCategory == null) return NotFound();

                return View(productCategory);
            }

            public IActionResult Delete(int id)
            {
                if (id == 0) return NotFound();

                var productCategory = _dbContext.ProductCategory.FirstOrDefault(p => p.Id == id);

                if (productCategory == null) return NotFound();


                return View(productCategory);
            }

            [HttpPost, ActionName("Delete")]
            public IActionResult DeleteConfirmed(int id)
            {
                if (id == 0) return NotFound();
                var productCategory = _dbContext.ProductCategory.FirstOrDefault(p => p.Id == id);

                if (productCategory == null) return NotFound();

                _dbContext.ProductCategory.Remove(productCategory);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
            }

            [HttpGet]
            public IActionResult Edit(int id, int productId)
            {
                if (id == 0) return NotFound();

                var productCategory = _dbContext.ProductCategory.FirstOrDefault(pc => pc.Id == id);

                if (productCategory == null) return NotFound();

                ViewBag.ProductId = productId;

                ViewBag.Categories = _dbContext.Category.Select
                    (
                     c => new SelectListItem()
                     {
                         Value = c.Id.ToString(),
                         Text = c.Title
                     }
                    ).ToList();

                ViewBag.Products = _dbContext.Products.Select
                    (
                        p => new SelectListItem()
                        {
                            Value = p.Id.ToString(),
                            Text = p.Title
                        }
                    ).ToList();

                return View(productCategory);
            }

            [HttpPost]
            public IActionResult Edit(ProductCategory productCategory)
            {
                if (productCategory == null || productCategory.Id == 0) return NotFound();

                if (ModelState.IsValid)
                {
                    _dbContext.ProductCategory.Update(productCategory);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
                }

                return View(productCategory);
            }

        
    }
}
