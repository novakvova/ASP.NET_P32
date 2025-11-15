using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkingMVC.Areas.Admin.Models;
using WorkingMVC.Constants;

namespace WorkingMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{Roles.Admin}")]
public class FormsController : Controller
{
  public IActionResult BasicInputs() => View();
  public IActionResult InputGroups() => View();
}
