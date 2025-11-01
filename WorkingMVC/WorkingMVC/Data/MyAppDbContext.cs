using WorkingMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WorkingMVC.Data;

public class MyAppDbContext : DbContext
{
    public MyAppDbContext(DbContextOptions<MyAppDbContext> dbContextOptions)
        : base(dbContextOptions)
    { }

    //Це таблиця в БД
    public DbSet<CategoryEntity> Categories { get; set; }
}
