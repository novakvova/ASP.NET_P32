using Microsoft.EntityFrameworkCore;
using WorkingMVC.Data.Entities;
using WorkingMVC.Interfaces;

namespace WorkingMVC.Repositories;

public class CategoryRepository : BaseRepository<CategoryEntity>, 
    ICategoryRepository
{
    public CategoryRepository(DbContext dbContext)
        : base(dbContext)
    { }
}
