namespace Core.Models.Location.Country;

public class CountryItemModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Image { get; set; }
}
