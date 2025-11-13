using System.ComponentModel.DataAnnotations;

namespace WorkingMVC.Models.Users;

public class UserItemModel
{
    [Display(Name = "#")]
    public int Id { get; set; }
    [Display(Name = "ПІБ")]
    public string FullName { get; set; } = string.Empty;
    [Display(Name = "Електронна адреса")]
    public string Email { get; set; } = string.Empty;
    [Display(Name = "Фото")]
    public string Image { get; set; } = string.Empty;
    [Display(Name = "Ролі")]
    public string[]? Roles { get; set; }
}
