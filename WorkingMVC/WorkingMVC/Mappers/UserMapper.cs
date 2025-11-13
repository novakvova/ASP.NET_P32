using AutoMapper;
using WorkingMVC.Data.Entities.Idenity;
using WorkingMVC.Models.Users;

namespace WorkingMVC.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<UserEntity, UserItemModel>()
            .ForMember(x => x.FullName, opt =>
                opt.MapFrom(x => $"{x.LastName} {x.FirstName}"))
            .ForMember(x => x.Image, opt =>
                opt.MapFrom(x => x.Image ?? "default.webp"))
            .ForMember(x => x.Roles, opt =>
                opt.MapFrom(x => x.UserRoles.Select(x=>x.Role.Name).ToArray()));
    }
}
