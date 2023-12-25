using FluentValidation;
using FreeEducation.Web.Models.Discounts;

namespace FreeEducation.Web.Validators;

public class DiscountApplyInputValidator :AbstractValidator<DiscountApplyInput>
{
    public DiscountApplyInputValidator()
    {
        RuleFor(x => x.Code).NotEmpty().WithMessage("{property} boş olamaz");
    }
}