using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Constants;
using WorkingMVC.Interfaces;

namespace WorkingMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{Roles.Admin}")]
public class UsersController(IUserService userService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await userService.GetUsersAsync();
        return View(result);
    }
}
