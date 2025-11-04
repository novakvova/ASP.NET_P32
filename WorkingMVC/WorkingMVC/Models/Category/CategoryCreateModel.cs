using System.ComponentModel.DataAnnotations;

namespace WorkingMVC.Models.Category;

public class CategoryCreateModel
{
    //Назва категорії
    [Display(Name = "Назва")]
    [Required(ErrorMessage = "Вкажіть назву категорії")]
    public string Name { get; set; } = string.Empty;
    //Передача на сервер фото
    [Display(Name = "Фото")]
    [Required(ErrorMessage ="Оберіть фото для категорії")]
    public IFormFile? Image { get; set; }
}