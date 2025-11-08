using WorkingMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WorkingMVC.Data.Entities.Idenity;
using Microsoft.AspNetCore.Identity;

namespace WorkingMVC.Data;

public class MyAppDbContext : IdentityDbContext<UserEntity, RoleEntity, int,
    IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public MyAppDbContext(DbContextOptions<MyAppDbContext> dbContextOptions)
        : base(dbContextOptions)
    { }

    //Це таблиця в БД
    public DbSet<CategoryEntity> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //
        builder.Entity<UserRoleEntity>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder.Entity<UserRoleEntity>()
            .HasOne(ur => ur.Role)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}
