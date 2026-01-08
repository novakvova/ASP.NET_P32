using AutoMapper;
using Core.Models.Cart;
using Domain.Entities;
namespace Core.Mappers;

public class CartMapper : Profile
{
    public CartMapper()
    {
        CreateMap<CartEntity, CartItemModel>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Transportation!.Id))
           .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Transportation!.Code))
           .ForMember(dest => dest.SeatsAvailable, opt => opt.MapFrom(src => src.Transportation!.SeatsAvailable))
           .ForMember(dest => dest.SeatsTotal, opt => opt.MapFrom(src => src.Transportation!.SeatsTotal))
           .ForMember(dest => dest.FromCityName, opt => opt.MapFrom(src => src.Transportation!.FromCity.Name))
           .ForMember(dest => dest.FromCountryName, opt => opt.MapFrom(src => src.Transportation!.FromCity.Country.Name))
           .ForMember(dest => dest.ToCityName, opt => opt.MapFrom(src => src.Transportation!.ToCity.Name))
           .ForMember(dest => dest.ToCountryName, opt => opt.MapFrom(src => src.Transportation!.ToCity.Country.Name))
           .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom(src => src.Transportation!.DepartureTime.ToString("dd.MM.yyyy HH:mm")))
           .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src => src.Transportation!.ArrivalTime.ToString("dd.MM.yyyy HH:mm")))
           .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Transportation!.Status.Name))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.CountTikets));
    }
}
