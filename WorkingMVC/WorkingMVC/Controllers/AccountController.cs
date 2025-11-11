using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Data.Entities.Idenity;
using WorkingMVC.Interfaces;
using WorkingMVC.Models.Account;

namespace WorkingMVC.Controllers;

public class AccountController(
    UserManager<UserEntity> userManager,
    SignInManager<UserEntity> signInManager,
    IImageService imageService,
    IMapper mapper) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if(!ModelState.IsValid)
            return View(model);
        var user = await userManager.FindByEmailAsync(model.Email);
        if(user!=null)
        {
            var res = await signInManager
                .PasswordSignInAsync(user, model.Password, false, false);
            if(res.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Redirect("/");
            }
        }
        ModelState.AddModelError("", "Дані вазано не вірно!");
        return View(model);
    }

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
            //після реєстрації авторизовуємо
            await signInManager.SignInAsync(user, isPersistent: false);
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

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Redirect("/");
    }

}
