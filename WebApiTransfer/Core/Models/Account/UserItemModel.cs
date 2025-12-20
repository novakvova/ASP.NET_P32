namespace Core.Models.Account;

public class UserItemModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}
