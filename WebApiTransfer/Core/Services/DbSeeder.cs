using Bogus;
using Core.Interfaces;
using Domain;
using Domain.Entities;
using Domain.Entities.Idenity;
using Domain.Entities.Location;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Core.Services;

public static class DbSeeder
{
    public static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<AppDbTransferContext>();
            var userManager = services.GetRequiredService<UserManager<UserEntity>>();
            var roleManager = services.GetRequiredService<RoleManager<RoleEntity>>();
            var imageService = services.GetRequiredService<IImageService>();
            var config = services.GetRequiredService<IConfiguration>();
            var env = services.GetRequiredService<Microsoft.AspNetCore.Hosting.IWebHostEnvironment>();

            await context.Database.MigrateAsync();

            var dirName = config.GetValue<string>("DirImageName") ?? "images";
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), dirName);
            if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);
            var contentRoot = env.ContentRootPath;
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (!context.Countries.Any())
            {
                var path = Path.Combine(contentRoot, "JSON", "countries.json");
                if (File.Exists(path))
                {
                    var data = await File.ReadAllTextAsync(path);
                    var items = JsonSerializer.Deserialize<List<CountryEntity>>(data, jsonOptions);
                    if (items != null)
                    {
                        foreach (var item in items) { item.DateCreated = DateTime.UtcNow; item.IsDeleted = false; item.Image = await ProcessImage(item.Image, imageService); }
                        await context.Countries.AddRangeAsync(items);
                        await context.SaveChangesAsync();

                    }
                }
            }

            if (!context.Cities.Any())
            {
                var path = Path.Combine(contentRoot, "JSON", "cities.json");
                if (File.Exists(path))
                {
                    var data = await File.ReadAllTextAsync(path);
                    var items = JsonSerializer.Deserialize<List<CityEntity>>(data, jsonOptions);
                    if (items != null)
                    {
                        foreach (var item in items) { item.DateCreated = DateTime.UtcNow; item.IsDeleted = false; item.Image = await ProcessImage(item.Image, imageService); }
                        await context.Cities.AddRangeAsync(items);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.TransportationStatuses.Any())
            {
                var path = Path.Combine(contentRoot, "JSON", "transportation_statuses.json");
                if (File.Exists(path))
                {
                    var data = await File.ReadAllTextAsync(path);
                    var items = JsonSerializer.Deserialize<List<TransportationStatusEntity>>(data, jsonOptions);
                    if (items != null)
                    {
                        foreach (var item in items) { item.DateCreated = DateTime.UtcNow; item.IsDeleted = false; }
                        await context.TransportationStatuses.AddRangeAsync(items);
                        await context.SaveChangesAsync();
                    }
                }
            }
            if (!context.Transportations.Any() && context.Cities.Any() && context.TransportationStatuses.Any())
            {
                var path = Path.Combine(contentRoot, "JSON", "transportation.json");
                if (File.Exists(path))
                {
                    var data = await File.ReadAllTextAsync(path);
                    var items = JsonSerializer.Deserialize<List<TransportationEntity>>(data, jsonOptions);
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            item.DateCreated = DateTime.UtcNow;
                            item.IsDeleted = false;
                            item.DepartureTime = DateTime.SpecifyKind(item.DepartureTime, DateTimeKind.Utc);
                            item.ArrivalTime = DateTime.SpecifyKind(item.ArrivalTime, DateTimeKind.Utc);
                        }
                        await context.Transportations.AddRangeAsync(items);
                        await context.SaveChangesAsync();
                    }
                }
            }
            var roles = new[] { "User", "Admin" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new RoleEntity { Name = role });
            }

            if (!context.Users.Any())
            {
                var adminUser = new UserEntity
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    FirstName = "System",
                    LastName = "Administrator",
                    Image = "default.jpg",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123");
                if (result.Succeeded) await userManager.AddToRoleAsync(adminUser, "Admin");
                var pathUsers = Path.Combine(contentRoot, "JSON", "users.json");
                int countUsers = 100;
                var faker = new Faker("uk");
                for (int i = 0; i < countUsers; i++)
                {
                    var firstName = faker.Name.FirstName();
                    var lastName = faker.Name.LastName();
                    var email = faker.Internet.Email(firstName, lastName);
                    var user = new UserEntity
                    {
                        UserName = email,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        Image = "default.jpg"
                    };
                    var userResult = await userManager.CreateAsync(user, "User123");
                    if (userResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                }

                if (File.Exists(pathUsers))
                {
                    var data = await File.ReadAllTextAsync(pathUsers);
                    var jsonArray = JsonNode.Parse(data)?.AsArray();

                    if (jsonArray != null)
                    {
                        foreach (var item in jsonArray)
                        {
                            var email = item["Email"]?.ToString();
                            var role = item["Role"]?.ToString() ?? "User";
                            var password = item["Password"]?.ToString() ?? "User123!";

                            if (!string.IsNullOrEmpty(email))
                            {
                                var user = new UserEntity
                                {
                                    UserName = email,
                                    Email = email,
                                    FirstName = item["FirstName"]?.ToString(),
                                    LastName = item["LastName"]?.ToString(),
                                    Image = item["Image"]?.ToString(),
                                    EmailConfirmed = true
                                };

                                var createRes = await userManager.CreateAsync(user, password);
                                if (createRes.Succeeded)
                                {
                                    await userManager.AddToRoleAsync(user, role);
                                }
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка під час сідінгу БД: {ex.Message}");
        }
    }
    private static async Task<string> ProcessImage(string? imageUrl, IImageService imageService)
    {
        if (string.IsNullOrEmpty(imageUrl) || !imageUrl.StartsWith("http"))
        {
            return "default.jpg";
        }

        try
        {
            using var client = new HttpClient();
            var fileBytes = await client.GetByteArrayAsync(imageUrl);
            return await imageService.SaveImageAsync(fileBytes);
        }
        catch (Exception)
        {
            return "default.jpg";
        }
    }
}
