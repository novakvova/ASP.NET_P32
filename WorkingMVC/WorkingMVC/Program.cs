using WorkingMVC.Data;
using WorkingMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WorkingMVC.Interfaces;
using WorkingMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyAppDbContext>(opt => 
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IImageService, ImageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

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
}

app.Run();
