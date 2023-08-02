using EcommAlgebra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommAlgebra.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        public IActionResult Update(string id) {

            var user = _context.Users.FirstOrDefault(user => user.Id == id);

            if (user == null) return NotFound();
            
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(ApplicationUser user) {

            var existingUser = _context.Users.FirstOrDefault(dbUser => dbUser.Id == user.Id);

            if(existingUser == null) return NotFound();

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;    

            _context.Users.Update(existingUser);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
