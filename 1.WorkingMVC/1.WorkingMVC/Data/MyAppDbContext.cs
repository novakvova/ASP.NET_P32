using _1.WorkingMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace _1.WorkingMVC.Data;

public class MyAppDbContext : DbContext
{
    public MyAppDbContext(DbContextOptions<MyAppDbContext> dbContextOptions)
        : base(dbContextOptions)
    { }

    //Це таблиця в БД
    public DbSet<CategoryEntity> Categories { get; set; }
}
