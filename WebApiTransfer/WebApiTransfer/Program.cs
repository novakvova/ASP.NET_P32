using Core.Interfaces;
using Core.Services;
using Domain;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApiTransfer.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbTransferContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IImageService, ImageService>();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMvc(options =>
{
    options.Filters.Add<ValidationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

var dirImageName = builder.Configuration
    .GetValue<string>("DirImageName") ?? "duplo";

// Console.WriteLine("Image dir {0}", dirImageName);
var path = Path.Combine(Directory.GetCurrentDirectory(), dirImageName);
Directory.CreateDirectory(dirImageName);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(path),
    RequestPath = $"/{dirImageName}"
});

app.Run();
