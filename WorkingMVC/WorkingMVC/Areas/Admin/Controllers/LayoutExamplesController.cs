using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WorkingMVC.Areas.Admin.Models;
using WorkingMVC.Constants;

namespace WorkingMVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = $"{Roles.Admin}")]
public class LayoutExamplesController : Controller
{
  public IActionResult Blank() => View();
  public IActionResult Container() => View();
  public IActionResult Fluid() => View();
  public IActionResult HorizontalMenu() => View();
  public IActionResult WithoutMenu() => View();
  public IActionResult WithoutNavbar() => View();
}
