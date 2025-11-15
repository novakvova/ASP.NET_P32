using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WorkingMVC.Constants;
using WorkingMVC.Data;
using WorkingMVC.Data.Entities.Idenity;
using WorkingMVC.Interfaces;
using WorkingMVC.Repositories;
using WorkingMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyAppDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<UserEntity, RoleEntity>(options =>
{
    options.Password.RequireDigit = false; //цифри
    options.Password.RequireNonAlphanumeric = false; //не букви і не цифри - спец
    options.Password.RequireLowercase = false; //маленькі
    options.Password.RequireUppercase = false; //великі
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1; //кількість різних символів
})
    .AddEntityFrameworkStores<MyAppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IImageService, ImageService>();
//builder.Services.AddTransient<IImageService, ImageService>();
//builder.Services.AddSingleton<IImageService, ImageService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IImageService, ImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

//Це означає, що у нас є авторизація і буде працювати
//signinManager
app.UseAuthorization();

app.MapStaticAssets();

app.MapAreaControllerRoute(
    name: "MyAreaPigAdmin",
    areaName: "Admin",
    pattern: "admin/{controller=Dashboards}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}")
    .WithStaticAssets();

var dirImageName = builder.Configuration.GetValue<string>("DirImageName") ?? "test";

// Console.WriteLine("Image dir {0}", dirImageName);
var path = Path.Combine(Directory.GetCurrentDirectory(), dirImageName);
Directory.CreateDirectory(dirImageName);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(path),
    RequestPath = $"/{dirImageName}"
});


using (var scoped = app.Services.CreateScope())
{
    var myAppDbContext = scoped.ServiceProvider.GetRequiredService<MyAppDbContext>();
    var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();
    myAppDbContext.Database.Migrate(); //якщо ми не робили міграціії

    if(!myAppDbContext.Categories.Any())
    {
        //var categories = new List<CategoryEntity>
        //{
        //    new CategoryEntity 
        //    { 
        //        Name = "Напої безалкогольні", 
        //        Image = "https://src.zakaz.atbmarket.com/cache/category/%D0%91%D0%B5%D0%B7%D0%B0%D0%BB%D0%BA%D0%BE%D0%B3%D0%BE%D0%BB%D1%8C%D0%BD%D1%96%20%D0%BD%D0%B0%D0%BF%D0%BE%D1%96%CC%88.webp"
        //    },
        //    new CategoryEntity
        //    {
        //        Name = "Овочі та фрукти",
        //        Image = "https://src.zakaz.atbmarket.com/cache/category/%D0%9E%D0%B2%D0%BE%D1%87%D1%96%20%D1%82%D0%B0%20%D1%84%D1%80%D1%83%D0%BA%D1%82%D0%B8.webp"
        //    }
        //};
        //myAppDbContext.Categories.AddRange(categories);
        //myAppDbContext.SaveChanges();
    }

    if(!myAppDbContext.Roles.Any()) //Якщо в БД немає ролей
    {
        foreach(var roleName in Roles.AllRoles)
        {
            var role = new RoleEntity(roleName);
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                Console.WriteLine($"-----Створили роль {roleName}-----");
            }
            else
            {
                foreach(var error in  result.Errors)
                {
                    Console.WriteLine("+++Проблема "+error.Description);
                }
                Console.WriteLine($"++++Проблеми створення ролі {roleName}++++");
            }
        }
    }
}

app.Run();
