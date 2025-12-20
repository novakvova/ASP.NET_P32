using Core.Models.Account;
using Core.Models.Search;

namespace Core.Interfaces;

public interface IUserService
{
    Task<UserProfileModel> GetUserProfileAsync();
    public Task<bool> ForgotPasswordAsync(ForgotPasswordModel model);
    public Task<bool> ResetPasswordAsync(ResetPasswordModel model);
    public Task<SearchResult<UserItemModel>> SearchAsync(UserSearchModel model);
}
