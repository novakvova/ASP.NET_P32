using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Location;

[Table("tblCities")]
public class CityEntity : BaseEntity<int>
{
    [StringLength(250)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    public string Slug { get; set; } = null!;

    public string? Image { get; set; }

    public string? Description { get; set; }

    // Зовнішній ключ на країну
    [ForeignKey(nameof(Country))]
    public int CountryId { get; set; }

    public CountryEntity Country { get; set; } = null!;

    public ICollection<TransportationEntity> Departures { get; set; }
    public ICollection<TransportationEntity> Arrivals { get; set; }

}
