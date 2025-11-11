using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkingMVC.Data.Entities.Idenity;
using WorkingMVC.Models.Account;

namespace WorkingMVC.ViewComponents;

public class UserLinkViewComponent(UserManager<UserEntity> userManager,
    IMapper mapper) : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var userName = User.Identity?.Name;
        var model = new UserLinkViewModel();

        if(userName != null)
        {
            var user = userManager.FindByEmailAsync(userName).Result;
            model = mapper.Map<UserLinkViewModel>(user);
        }

        return View(model);
    }
}
