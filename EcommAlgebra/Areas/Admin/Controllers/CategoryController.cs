﻿using EcommAlgebra.Data;
using EcommAlgebra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EcommAlgebra.Areas.Admin.Controllers
{
   
        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        public class CategoryController : Controller
        {
            private ApplicationDbContext _dbContext;

            public CategoryController(ApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public IActionResult Index()
            {
                var categories = _dbContext.Category.ToList();

                return View(categories);

            }

            public IActionResult Details(int id)
            {
                if (id == 0)
                {
                    return NotFound();
                }

                var category = _dbContext.Category.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(Category category)
            {
                // model state provjerava ispravnost modela (u ovom slučaju category)
                if (ModelState.IsValid)
                {
                    _dbContext.Category.Add(category);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                return View(category);
            }

            public IActionResult Edit(int? id)
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }

                var category = _dbContext.Category.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }

            [HttpPost]
            public IActionResult Edit(Category category)
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Update(category);
                    _dbContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }

                return View(category);
            }

            // HTTP GET /Category/Delete
            public IActionResult Delete(int id)
            {
                if (id == 0)
                {
                    return NotFound();
                }

                var category = _dbContext.Category.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }

            [HttpPost]
            // HTTP POST /Category/Delete
            [ActionName("Delete")]
            // HTTP POST /Category/DeleteConfirmed
            public IActionResult DeleteConfirmed(int id)
            {
                if (id == 0)
                {
                    return NotFound();
                }

                var category = _dbContext.Category.FirstOrDefault(c => c.Id == id);

                if (category == null)
                {
                    return NotFound();
                }

                _dbContext.Category.Remove(category);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
        }
    
}
