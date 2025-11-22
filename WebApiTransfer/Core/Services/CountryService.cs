using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Location;
using Domain;
using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class CountryService(AppDbTransferContext appDbContext, 
    IImageService imageService,
    IMapper mapper) : ICountryService
{
    public async Task<CountryItemModel> CreateAsync(CountryCreateModel model)
    {
        var entity = mapper.Map<CountryEntity>(model);
        if (model.Image != null)
        {
            entity.Image = await imageService.UploadImageAsync(model.Image);
        }
        await appDbContext.Countries.AddAsync(entity);
        await appDbContext.SaveChangesAsync();
        var item = mapper.Map<CountryItemModel>(entity);
        return item;
    }

    public async Task<List<CountryItemModel>> GetListAsync()
    {
        var list  = await appDbContext.Countries
            .ProjectTo<CountryItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }
}
