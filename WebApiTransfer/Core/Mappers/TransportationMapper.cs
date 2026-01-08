using AutoMapper;
using Core.Models.Transportation;
using Domain.Entities;

namespace Core.Mappers;

public class TransportationMapper : Profile
{
    public TransportationMapper()
    {
        CreateMap<TransportationEntity, TransportationItemModel>()
            .ForMember(dest => dest.FromCityName, opt => opt.MapFrom(src => src.FromCity.Name))
            .ForMember(dest => dest.FromCountryName, opt => opt.MapFrom(src => src.FromCity.Country.Name))
            .ForMember(dest => dest.ToCityName, opt => opt.MapFrom(src => src.ToCity.Name))
            .ForMember(dest => dest.ToCountryName, opt => opt.MapFrom(src => src.ToCity.Country.Name))
            .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom(src => src.DepartureTime.ToString("dd.MM.yyyy HH:mm")))
            .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src => src.ArrivalTime.ToString("dd.MM.yyyy HH:mm")))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));
    }
}
