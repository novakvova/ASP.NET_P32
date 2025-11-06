using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Interfaces;
using WorkingMVC.Models.Category;

namespace WorkingMVC.Controllers;

//.NEt 8.0 та 9.0
public class MainController(ICategoryService categoryService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var model = await categoryService.GetAllAsync();
        return View(model);
    }

    //Для того, щоб побачити сторінку створення категорії
    [HttpGet] //Щоб побачити сторінку і внести інформацію про категорію
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost] //Збереження даних
    public async Task<IActionResult> Create(CategoryCreateModel model)
    {
        if(!ModelState.IsValid)
        {
            return View(model); // якщо модель не валідна викидаємо дані назад,
            //Щоб користувач знав, що він невірно вніс
        }

        try
        {
            await categoryService.CreateAsync(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(model);
        }
        return RedirectToAction(nameof(Index));
    }
}
