using WorkingMVC.Data;
using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Data.Entities;
using WorkingMVC.Models.Category;

namespace WorkingMVC.Controllers;

//.NEt 8.0 та 9.0
public class MainController(MyAppDbContext myAppDbContext,
    IConfiguration configuration) : Controller
{
    public IActionResult Index()
    {
        var list = myAppDbContext.Categories.ToList();
        return View(list);
    }

    //Для того, щоб побачити сторінку створення категорії
    [HttpGet] //Щоб побачити сторінку і внести інформацію про категорію
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost] //Збереження даних
    public IActionResult Create(CategoryCreateModel model)
    {
        var entity = new CategoryEntity
        {
            Name = model.Name
        };
        var dirImageName = configuration.GetValue<string>("DirImageName");
        if (model.Image != null)
        {
            //Guid - генерує випадкову величну, яка не можу повторитися
            var fileName = Guid.NewGuid().ToString()+".jpg";
            var pathSave = Path.Combine(Directory.GetCurrentDirectory(), 
                dirImageName ?? "images", fileName);
            using var stream = new FileStream(pathSave, FileMode.Create);
            model.Image.CopyTo(stream); //Зберігаємо фото, яке на приходить у папку.
            entity.Image = fileName;
        }
        myAppDbContext.Categories.Add(entity);
        myAppDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
