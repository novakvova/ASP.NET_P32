using Core.Models.Account;

namespace Core.Interfaces;

public interface IUserService
{
    Task<UserProfileModel> GetUserProfileAsync();
    public Task<bool> ForgotPasswordAsync(ForgotPasswordModel model);
    public Task<bool> ResetPasswordAsync(ResetPasswordModel model);
}
