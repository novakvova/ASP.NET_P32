using Core.Interfaces;
using Core.Models.Account;
using Core.Services;
using Domain.Entities.Idenity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApiTransfer.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController(UserManager<UserEntity> userManager,
    IUserService userService,
    RoleManager<RoleEntity> roleManager,
    IImageService imageService,
    IJwtTokenService jwtTokenService) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized("Invalid email or password.");
        }
        var token = await jwtTokenService.CreateAsync(user);
        return Ok(new { token });
    }

    /*Відновлення паролю
     1.Спочатку користувач вказу пошту
     2.На пошту приходить лист, де ми можемо перейти на відновлення
     2.1. Дане посилання має бути на frontend частину, 
        де користувач зможе ввести новий пароль.
     2.2. Коли користувач вводить новий пароль, 
        frontend частина має відправити запит на backend 
        з новим паролем та токеном(токен - ключ для відновлення паролю).
    3. Тобто при відновлені пролю миємо знати хто є клієнт, який
        хоче відновити пароль (тобто домен клієнта react).
    */

    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
    {
        bool res = await userService.ForgotPasswordAsync(model);
        if (res)
            return Ok();
        else
            return BadRequest(new
            {
                Status = 400,
                IsValid = false,
                Errors = new { Email = "Користувача з такою поштою не існує" }
            });
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        var isTry =  await userService.ResetPasswordAsync(model);
        if (!isTry)
        {
            return BadRequest(new
            {
                Status = 400,
                IsValid = false,
                Errors = new { Email = "Невірні дані для відновлення паролю" }
            });
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var entity = new UserEntity
        {
            Email = model.Email,
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };
        if (model.Image != null)
        {
            entity.Image = await imageService.UploadImageAsync(model.Image);
        }

        var result = await userManager.CreateAsync(entity, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            await roleManager.CreateAsync(new RoleEntity { Name = "User" });
        }
        await userManager.AddToRoleAsync(entity, "User");

        var token = await jwtTokenService.CreateAsync(entity);

        return Ok(new { token });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var model =  await userService.GetUserProfileAsync();
        return Ok(model);
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] UserSearchModel model)
    {
        //Обчислення часу виконання
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var result = await userService.SearchAsync(model);
        stopwatch.Stop();
        // Get the elapsed time as a TimeSpan value.
        TimeSpan ts = stopwatch.Elapsed;
        // Format and display the TimeSpan value.
        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("-----------Elapsed Time------------: " + elapsedTime);
        return Ok(result);
    }
}
