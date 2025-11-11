using AutoMapper;
using WorkingMVC.Data.Entities.Idenity;
using WorkingMVC.Models.Account;

namespace WorkingMVC.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<RegisterViewModel, UserEntity>()
            .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Email))
            .ForMember(x => x.Image, opt => opt.Ignore());

        CreateMap<UserEntity, UserLinkViewModel>()
            .ForMember(x => x.Name, opt =>
                opt.MapFrom(x => $"{x.LastName} {x.FirstName}"))
            .ForMember(x => x.Image, opt =>
                opt.MapFrom(x => x.Image ?? "default.webp"));
    }
}
