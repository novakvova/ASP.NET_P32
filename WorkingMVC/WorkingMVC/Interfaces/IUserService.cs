using WorkingMVC.Areas.Admin.Models.Users;

namespace WorkingMVC.Interfaces;

public interface IUserService
{
    Task<List<UserItemModel>> GetUsersAsync();
}
