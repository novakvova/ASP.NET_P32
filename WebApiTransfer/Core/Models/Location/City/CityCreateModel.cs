using Microsoft.AspNetCore.Http;

namespace Core.Models.Location.City;

public class CityCreateModel
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public int CountryId { get; set; }
    public IFormFile Image { get; set; } = null!;
}
