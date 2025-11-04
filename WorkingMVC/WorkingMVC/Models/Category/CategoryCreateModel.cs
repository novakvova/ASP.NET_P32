using System.ComponentModel.DataAnnotations;

namespace WorkingMVC.Models.Category;

public class CategoryCreateModel
{
    //Назва категорії
    [Display(Name = "Назва")]
    public string Name { get; set; } = string.Empty;
    //Передача на сервер фото
    [Display(Name = "Фото")]
    public IFormFile? Image { get; set; }
}