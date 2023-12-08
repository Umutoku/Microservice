namespace FreeEducation.Web.Models.Baskets;

public class BasketViewModel
{
    public string UserId { get; set; }
    public string DiscountCode { get; set; }
    
    public int? DiscountRate { get; set; }
    private List<BasketItemViewModel> basketItems { get; set; }

    public List<BasketItemViewModel> BasketItems
    {
        get
        {
            if (HasDiscount)
            {
                foreach (var basketItemViewModel in basketItems)
                {
                    var discountPrice = basketItemViewModel.Price*((decimal)DiscountRate.Value /100);
                    basketItemViewModel.AppliedDiscount(Math.Round(basketItemViewModel.Price-discountPrice,2));
                }
            }

            return basketItems;
        }
        set
        {
            basketItems = value;
        }
    }

    public decimal TotalPrice
    {
        get => basketItems.Sum(x => x.GetCurrentPrice);
    }

    public bool HasDiscount
    {
        get => !string.IsNullOrEmpty(DiscountCode);
    }
}