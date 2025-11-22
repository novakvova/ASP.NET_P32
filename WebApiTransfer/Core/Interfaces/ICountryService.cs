using Core.Models.Location;

namespace Core.Interfaces;

public interface ICountryService
{
    Task<List<CountryItemModel>> GetListAsync();
    Task<CountryItemModel> CreateAsync(CountryCreateModel model);
}
