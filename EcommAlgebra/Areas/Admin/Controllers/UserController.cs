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
    }
}
