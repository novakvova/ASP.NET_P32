using Microsoft.AspNetCore.Mvc;

namespace _1.WorkingMVC.Controllers;

public class MainController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
