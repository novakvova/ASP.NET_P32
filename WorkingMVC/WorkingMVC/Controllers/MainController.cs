using WorkingMVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace WorkingMVC.Controllers;

//.NEt 8.0 та 9.0
public class MainController(MyAppDbContext myAppDbContext) : Controller
{
    public IActionResult Index()
    {
        var list = myAppDbContext.Categories.ToList();
        return View(list);
    }
}
