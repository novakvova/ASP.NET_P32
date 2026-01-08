using Domain.Entities.Location;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("tblTransportations")]
public class TransportationEntity : BaseEntity<int>
{
    /// <summary>
    /// Код перевезення - PS101, PS505
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Звідки ми виїзджаємо
    /// </summary>
    [ForeignKey(nameof(FromCity))]
    public int FromCityId { get; set; }
    public CityEntity FromCity { get; set; }

    /// <summary>
    /// Куди ми приїзджаємо
    /// </summary>
    [ForeignKey(nameof(ToCity))]
    public int ToCityId { get; set; }
    public CityEntity ToCity { get; set; }

    /// <summary>
    /// Дата і час виїзду
    /// </summary>
    public DateTime DepartureTime { get; set; }
    /// <summary>
    /// Дата і час прибування
    /// </summary>
    public DateTime ArrivalTime { get; set; }

    /// <summary>
    /// Загальна кількість місць
    /// </summary>
    public int SeatsTotal { get; set; }

    /// <summary>
    /// Кількість вільних (ще не зайнятих) місць
    /// </summary>
    public int SeatsAvailable { get; set; }

    /// <summary>
    /// У якому статусі знаходиться рейс - 
    /// запланований
    /// затримується
    /// скасований
    /// виконаний
    /// немає місць
    /// </summary>
    [ForeignKey(nameof(Status))]
    public int StatusId { get; set; }
    public TransportationStatusEntity Status { get; set; }

    public ICollection<CartEntity> Carts { get; set; } = null!;

}
