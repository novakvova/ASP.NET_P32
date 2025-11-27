namespace Core.Models.Location.City;

public class CityItemModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
}
