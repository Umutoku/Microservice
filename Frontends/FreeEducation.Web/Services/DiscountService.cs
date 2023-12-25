using FreeEducation.Shared.Dtos;
using FreeEducation.Web.Models.Discounts;
using FreeEducation.Web.Services.Interfaces;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace FreeEducation.Web.Services;

public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;

    public DiscountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DiscountViewModel> GetDiscount(string discountCode)
    {
        var response = await _httpClient
            .GetAsync($"/Discounts/api/Discounts/GetByCode/{discountCode}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var discount = await response.Content.ReadFromJsonAsync<ResponseDto<DiscountViewModel>>();
        return discount.Data;
    }
}