using Microsoft.AspNetCore.Mvc;

namespace WorkingMVC.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
