using System.ComponentModel.DataAnnotations;

namespace WorkingMVC.Models.Account;

public class LoginViewModel
{
    [Display(Name = "Пошта")]
    [Required(ErrorMessage = "Вкажіть пошту")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Це поле обов'язкове!")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;
}
