using AutoMapper;
using Core.Models.Location.City;
using Domain.Entities.Location;

namespace Core.Mappers;

public class CityMapper : Profile
{
    public CityMapper()
    {
        CreateMap<CityEntity, CityItemModel>();

        CreateMap<CityCreateModel, CityEntity>()
            .ForMember(x=>x.Image, opt=>opt.Ignore());
    }
}
