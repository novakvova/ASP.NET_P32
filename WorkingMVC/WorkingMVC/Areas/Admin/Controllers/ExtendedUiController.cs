using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkingMVC.Areas.Admin.Models;
using WorkingMVC.Constants;

namespace WorkingMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{Roles.Admin}")]
public class ExtendedUiController : Controller
{
  public IActionResult PerfectScrollbar() => View();
  public IActionResult TextDivider() => View();
}
