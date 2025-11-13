using WorkingMVC.Models.Users;

namespace WorkingMVC.Interfaces;

public interface IUserService
{
    Task<List<UserItemModel>> GetUsersAsync();
}
