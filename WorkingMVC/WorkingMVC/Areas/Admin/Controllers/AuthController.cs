using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Constants;

namespace WorkingMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{Roles.Admin}")]
public class AuthController : Controller
{
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic() => View();
  public IActionResult RegisterBasic() => View();
}
