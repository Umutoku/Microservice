using FreeEducation.Web.Models.Discounts;

namespace FreeEducation.Web.Services.Interfaces;

public interface IDiscountService
{
    Task<DiscountViewModel> GetDiscount(string discountCode);
}