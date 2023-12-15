using FluentValidation;
using FreeEducation.Web.Models.Catalogs;

namespace FreeEducation.Web.Validators;

public class EducationUpdateInputValidation:AbstractValidator<EducationUpdateInput>
{
    public EducationUpdateInputValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("İsim boş olamaz");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama boş olamaz");
        RuleFor(x => x.Feature.Duration).InclusiveBetween(1, int.MaxValue).WithMessage("Süre boş olamaz");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat alanı boş olamaz").PrecisionScale(2,6,true).WithMessage("Hatalı para formatı");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("Categori id boş olamaz");
    }
}