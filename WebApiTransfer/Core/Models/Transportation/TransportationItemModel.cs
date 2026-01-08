namespace Core.Models.Transportation;


public class TransportationItemModel
{
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// Звідки ми виїзджаємо
    /// </summary>
    public string FromCityName { get; set; } = string.Empty;
    public string FromCountryName { get; set; } = string.Empty;

    /// <summary>
    /// Куди ми приїзджаємо
    /// </summary>
    public string ToCityName { get; set; } = string.Empty;
    public string ToCountryName { get; set; } = string.Empty;

    /// <summary>
    /// Дата і час виїзду
    /// </summary>
    public string DepartureTime { get; set; } = string.Empty;
    /// <summary>
    /// Дата і час прибування
    /// </summary>
    public string ArrivalTime { get; set; } = string.Empty;

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
    public string StatusName { get; set; } = string.Empty;
}
