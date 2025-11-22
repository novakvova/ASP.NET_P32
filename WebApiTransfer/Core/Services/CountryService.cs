using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Location;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class CountryService(AppDbTransferContext appDbContext, 
    IMapper mapper) : ICountryService
{
    public async Task<List<CountryItemModel>> GetListAsync()
    {
        var list  = await appDbContext.Countries
            .ProjectTo<CountryItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }
}
