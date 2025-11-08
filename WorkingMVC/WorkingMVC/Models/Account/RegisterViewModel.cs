using System.ComponentModel.DataAnnotations;

namespace WorkingMVC.Models.Account;

public class RegisterViewModel
{
    [Display(Name = "Прізвище")]
    [Required(ErrorMessage = "Вкажіть прізвище")]
    public string LastName { get; set; } = string.Empty;

    [Display(Name = "Ім'я")]
    [Required(ErrorMessage = "Вкажіть ім'я")]
    public string FirstName { get; set; } = string.Empty;

    [Display(Name = "Пошта")]
    [Required(ErrorMessage = "Вкажіть пошту")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Це поле обов'язкове!")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Це поле обов'язкове!")]
    [Compare("Password", ErrorMessage = "Паролі не співпадають")]
    [DataType(DataType.Password)]
    [Display(Name = "Підтвердити пароль")]
    public string PasswordConfirm { get; set; } = string.Empty;

    //Передача на сервер фото
    [Display(Name = "Фото")]
    [Required(ErrorMessage = "Оберіть фото для категорії")]
    public IFormFile? Image { get; set; }
}
