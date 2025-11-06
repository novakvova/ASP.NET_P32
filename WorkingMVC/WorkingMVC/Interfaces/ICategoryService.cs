using WorkingMVC.Models.Category;

namespace WorkingMVC.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryItemModel>> GetAllAsync();
    Task CreateAsync(CategoryCreateModel model);
}
