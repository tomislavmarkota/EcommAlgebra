using EcommAlgebra.Data;
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

        public UserController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            //var users = _context.Users.ToList();

            //return View(users);

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

        public IActionResult Update(string id) {

            //var user = _context.Users.FirstOrDefault(u => u.Id == id);
            //if (user == null) return NotFound();

            //var viewModel = new UserViewModel
            //{
            //    UserId = user.Id,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    Address = user.Address,
            //    AvailableRoles = _roleManager.Roles.ToList()
            //};
            //return View(viewModel);

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
