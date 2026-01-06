using Core.Models.Location.City;

namespace Core.Interfaces;

public interface ICityService
{
    Task<List<CityItemModel>> GetListAsync();
    Task<CityItemModel> CreateAsync(CityCreateModel model);
    Task<CityItemModel> GetByIdAsync(int id);
    Task<CityItemModel> DeleteAsync(int id);
}
