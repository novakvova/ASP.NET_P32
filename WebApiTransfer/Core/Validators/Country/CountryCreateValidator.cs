using Core.Models.Location.Country;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Core.Validators.Country;

public class CountryCreateValidator : AbstractValidator<CountryCreateModel>
{ 
    public CountryCreateValidator(AppDbTransferContext db)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва країни не може бути порожньою")
            .MaximumLength(100).WithMessage("Назва країни не може перевищувати 100 символів")
            .DependentRules(() =>
            {
                RuleFor(x => x.Name)
                    .MustAsync(async (name, cancellation) =>
                        !await db.Countries.AnyAsync(c => c.Name.ToLower() == name.ToLower().Trim(), cancellation))
                    .WithMessage("Країна з такою назвою вже існує");
            });

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Код країни не може бути порожнім")
            .MaximumLength(10).WithMessage("Код країни не може перевищувати 10 символів");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug країни не може бути порожнім")
            .MaximumLength(100).WithMessage("Slug країни не може перевищувати 100 символів");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Файл зображення є обов'язковим");
    }
}
