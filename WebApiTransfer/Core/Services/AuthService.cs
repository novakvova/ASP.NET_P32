using Core.Interfaces;
using Domain.Entities.Idenity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Core.Services;

public class AuthService(IHttpContextAccessor httpContextAccessor,
    UserManager<UserEntity> userManager) 
    : IAuthService
{
    public async Task<int> GetUserIdAsync()
    {
        var email = httpContextAccessor.HttpContext?.User.Claims.First().Value;
        if(string.IsNullOrEmpty(email))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
        var user = await userManager.FindByEmailAsync(email);
        return user!.Id;
    }
}
