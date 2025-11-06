using Microsoft.EntityFrameworkCore;
using WorkingMVC.Data;
using WorkingMVC.Data.Entities;
using WorkingMVC.Interfaces;

namespace WorkingMVC.Repositories;

public class CategoryRepository : BaseRepository<CategoryEntity>, 
    ICategoryRepository
{
    public CategoryRepository(MyAppDbContext dbContext)
        : base(dbContext)
    { }
}
