﻿using EcommAlgebra.Data;
using EcommAlgebra.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcommAlgebra.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }


   
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            var viewModelList = new List<UserViewModel>();

            foreach (var user in users)
            {
                var viewModel = new UserViewModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Address = user.Address,
                    AvailableRoles = _roleManager.Roles.ToList()
                };

                var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
                if (userRole != null)
                {
                    var role = _roleManager.Roles.FirstOrDefault(r => r.Id == userRole.RoleId);
                    if (role != null)
                    {
                        viewModel.Role = role.Name;
                    }
                }

                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                // Create a new ApplicationUser based on the ViewModel data
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email, // We use the email as the username for simplicity
                    Address = model.Address
                };

                // Create the user in the database
                var result = await _userManager.CreateAsync(user, model.PasswordHash);

                if (result.Succeeded)
                {
                    // Optionally, you can assign roles to the new user here
                    // For example, if you want to set the user as an "Admin" by default, you can do:
                    // await _userManager.AddToRoleAsync(user, "Admin");
                    // This requires you to have already created the "Admin" role.

                    // Initialize the user role (assuming you have a "User" role already created)
                    await _userManager.AddToRoleAsync(user, "User");

                    // Redirect to a success page or the list of users
                    return RedirectToAction(nameof(Index));
                }

                // If there are errors, add them to ModelState and show them to the user
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            // If ModelState is not valid, return to the same view with the provided data
            return RedirectToAction(nameof(Index));
        }






        public IActionResult Update(string id) {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            var viewModel = new UserViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                AvailableRoles = _roleManager.Roles.ToList()
            };

            // Set the selected role based on the user's current role
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.UserId == user.Id);
            if (userRole != null)
            {
                var role = _roleManager.Roles.FirstOrDefault(r => r.Id == userRole.RoleId);
                if (role != null)
                {
                    viewModel.SelectedRole = role.Name;
                }
            }

            return View(viewModel);


        }

        [HttpPost]
        public IActionResult Update(UserViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                viewModel.AvailableRoles = _roleManager.Roles.ToList();
                return View(viewModel);
            }

            var existingUser = _context.Users.FirstOrDefault(dbUser => dbUser.Id == viewModel.UserId);
            if (existingUser == null) return NotFound();

            existingUser.FirstName = viewModel.FirstName;
            existingUser.LastName = viewModel.LastName;
            existingUser.Email = viewModel.Email;
            existingUser.Address = viewModel.Address;

            // Update the user's role
            if (!string.IsNullOrEmpty(viewModel.SelectedRole))
            {
                var role = _roleManager.FindByNameAsync(viewModel.SelectedRole).Result;
                if (role != null)
                {
                    var userRoles = _context.UserRoles.Where(ur => ur.UserId == existingUser.Id).ToList();

                    // Remove existing roles (if any)
                    foreach (var userRole in userRoles)
                    {
                        var existingRole = _roleManager.Roles.FirstOrDefault(r => r.Id == userRole.RoleId);
                        if (existingRole != null)
                        {
                            _context.UserRoles.Remove(userRole);
                        }
                    }

                    // Add the selected role
                    _context.UserRoles.Add(new IdentityUserRole<string> { UserId = existingUser.Id, RoleId = role.Id });
                }
            }

            _context.Users.Update(existingUser);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


    }
}
