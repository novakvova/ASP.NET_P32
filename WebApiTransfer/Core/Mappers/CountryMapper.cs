using AutoMapper;
using Core.Models.Location;
using Domain.Entities.Location;

namespace Core.Mappers;

public class CountryMapper : Profile
{
    public CountryMapper()
    {
        CreateMap<CountryEntity, CountryItemModel>();

        CreateMap<CountryCreateModel, CountryEntity>()
            .ForMember(x=>x.Image, opt=>opt.Ignore());
    }
}
