using AutoMapper;
using WorkingMVC.Data;
using WorkingMVC.Data.Entities;
using WorkingMVC.Interfaces;
using WorkingMVC.Models.Category;

namespace WorkingMVC.Services;

public class CategoryService(ICategoryRepository categoryRepository,
    IImageService imageService,
    IMapper mapper) : ICategoryService
{
    public async Task CreateAsync(CategoryCreateModel model)
    {
        var entity = await categoryRepository.FindByNameAsync(model.Name);

        if (entity != null)
        {
            throw new Exception("У нас проблеми Хюстон" +
                $"Така категорія уже є {model.Name}");
        }
        entity = new CategoryEntity
        {
            Name = model.Name
        }; 
        if (model.Image != null)
        {
            entity.Image = await imageService.UploadImageAsync(model.Image);
        }
        await categoryRepository.AddAsync(entity);
    }

    public async Task<List<CategoryItemModel>> GetAllAsync()
    {
        var listTest = await categoryRepository.GetAllAsync();
        var model = mapper.Map<List<CategoryItemModel>>(listTest);
        return model;
    }
}
