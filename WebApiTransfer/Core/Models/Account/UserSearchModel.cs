namespace Core.Models.Account;

public class UserSearchModel
{
    //Пошук по Прізвищу або Імені
    public string? Name { get; set; }
    //Фільтр по Даті реєстрації
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    //Пагінація
    public int Page { get; set; } = 1; //Поточна сторінка
    public int ItemPerPage { get; set; } = 10; //Кількість елементів на сторінці
}
