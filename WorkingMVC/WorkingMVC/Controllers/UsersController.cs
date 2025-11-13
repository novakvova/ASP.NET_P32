using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Interfaces;

namespace WorkingMVC.Controllers
{
    public class UsersController(IUserService userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var result = await userService.GetUsersAsync();
            return View(result);
        }
    }
}
