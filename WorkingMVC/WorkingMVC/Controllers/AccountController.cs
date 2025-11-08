using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Data.Entities.Idenity;
using WorkingMVC.Interfaces;
using WorkingMVC.Models.Account;

namespace WorkingMVC.Controllers;

public class AccountController(
    UserManager<UserEntity> userManager,
    IImageService imageService,
    IMapper mapper) : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = mapper.Map<UserEntity>(model);

        var imageStr = model.Image is not null 
            ? await imageService.UploadImageAsync(model.Image) : null;

        user.Image = imageStr;
        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            //Перехід на головну
            return RedirectToAction("Index", "Main");
        }
        else
        {
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError(string.Empty, item.Description);
            }
            return View(model);
        }
    }
}
