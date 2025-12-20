namespace Core.Models.Search;

/// <summary>
/// Зберігає інформацію для пагінації результатів пошуку.
/// </summary>
/// <remarks>
/// Використовується для передачі метаданих про пагінацію (загальна кількість елементів,
/// кількість сторінок, елементів на сторінку та поточна сторінка).
/// Зазвичай: `TotalPages = (TotalCount + ItemsPerPage - 1) / ItemsPerPage`.
/// CurrentPage зазвичай індексується від 1.
/// </remarks>
public class PaginationModel
{
    /// <summary>
    /// Загальна кількість елементів, що відповідають критеріям пошуку.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Загальна кількість сторінок, доступних при поточному значенні <see cref="ItemsPerPage"/>.
    /// </summary>
    /// <remarks>
    /// Розраховується як округлення вгору від TotalCount / ItemsPerPage.
    /// Може бути 0, якщо TotalCount дорівнює 0.
    /// </remarks>
    public int TotalPages { get; set; }

    /// <summary>
    /// Кількість елементів на одну сторінку.
    /// </summary>
    /// <remarks>
    /// Очікується значення > 0. Якщо значення дорівнює 0 або від'ємне, слід обробити це на рівні логіки сервісу.
    /// </remarks>
    public int ItemsPerPage { get; set; }

    /// <summary>
    /// Номер поточної сторінки (індексація зазвичай від 1).
    /// </summary>
    /// <remarks>
    /// Якщо використовуєте нульову індексацію, конвертуйте відповідно у логіці виклику.
    /// </remarks>
    public int CurrentPage { get; set; }
}
