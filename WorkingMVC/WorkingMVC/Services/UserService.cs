using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WorkingMVC.Areas.Admin.Models.Users;
using WorkingMVC.Data;
using WorkingMVC.Interfaces;

namespace WorkingMVC.Services;

public class UserService(MyAppDbContext context,
    IMapper mapper) : IUserService
{
    public async Task<List<UserItemModel>> GetUsersAsync()
    {
        //Це sql запит
        var query = context.Users;
        var model = await query
            .ProjectTo<UserItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();

        return model;
    }
}
