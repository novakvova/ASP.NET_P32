using Microsoft.EntityFrameworkCore;
using WorkingMVC.Data;
using WorkingMVC.Data.Entities;
using WorkingMVC.Interfaces;

namespace WorkingMVC.Repositories;

public class CategoryRepository : BaseRepository<CategoryEntity, int>, 
    ICategoryRepository
{
    public CategoryRepository(MyAppDbContext dbContext)
        : base(dbContext)
    { }

    public async Task<CategoryEntity?> FindByNameAsync(string name)
    {
        var nameLower = name.Trim().ToLower();
        var entity = await _dbSet.SingleOrDefaultAsync(c => c.Name.ToLower() == nameLower);
        return entity;
    }
}
