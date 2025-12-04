using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Account;
using Core.Models.Location.City;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserService(IAuthService authService, 
    AppDbTransferContext transferContext,
    IMapper mapper) : IUserService
{
    public async Task<UserProfileModel> GetUserProfileAsync()
    {
        var userId = await authService.GetUserIdAsync();
        
        var profile = await transferContext.Users
            .ProjectTo<UserProfileModel>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(u => u.Id == userId!);

        return profile!;
    }
}
