namespace Core.Models.Search;

public class SearchResult<T>
{
    public List<T> Items { get; set; } = new(); // Елементи результату пошуку
    public PaginationModel Pagination { get; set; } = new(); // Інформація про пагінацію
}
