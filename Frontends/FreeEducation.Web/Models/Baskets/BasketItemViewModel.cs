namespace FreeEducation.Web.Models.Baskets;

public class BasketItemViewModel
{
    public int Quantity { get; set; } = 1;
    public string EducationId { get; set; }
    public string EducationName { get; set; }
    public decimal Price { get; set; }

    private decimal? DiscountAppliedPrice;

    public decimal GetCurrentPrice
    {
        get => DiscountAppliedPrice != null ? DiscountAppliedPrice.Value : Price;
    }

    public void AppliedDiscount(decimal discountPrice)
    {
        DiscountAppliedPrice = discountPrice;
    }
}