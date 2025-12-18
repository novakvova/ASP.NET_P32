using Core.Interfaces;
using Core.Models.Account;
using Domain.Entities.Idenity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTransfer.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController(UserManager<UserEntity> userManager,
    IUserService userService,
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


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var model =  await userService.GetUserProfileAsync();
        return Ok(model);
    }
}
