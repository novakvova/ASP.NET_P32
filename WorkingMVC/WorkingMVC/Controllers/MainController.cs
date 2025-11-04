using WorkingMVC.Data;
using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Data.Entities;
using WorkingMVC.Models.Category;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace WorkingMVC.Controllers;

//.NEt 8.0 та 9.0
public class MainController(MyAppDbContext myAppDbContext,
    IConfiguration configuration,
    IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var list = await myAppDbContext.Categories
            .ProjectTo<CategoryItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();
            ///.Select(x => new CategoryItemModel
            ///{
            ///    Id = x.Id,
            ///    Name = x.Name,
            ///    Image = x.Image
            ///})
            ///.ToList();
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
        if(!ModelState.IsValid)
        {
            return View(model); // якщо модель не валідна викидаємо дані назад,
            //Щоб користувач знав, що він невірно вніс
        }

        var name = model.Name.Trim().ToLower();
        var entity = myAppDbContext.Categories
            .SingleOrDefault(c => c.Name.ToLower() == name);

        if (entity != null)
        {
            ModelState.AddModelError("", "У нас проблеми Хюстон" +
                $"Така категорія уже є {name}");
            return View(model);
        }

        entity = new CategoryEntity
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
