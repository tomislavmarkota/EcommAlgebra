using Microsoft.AspNetCore.Mvc;

namespace EcommAlgebra.Areas.Admin.Controllers
{
    public class UserManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
