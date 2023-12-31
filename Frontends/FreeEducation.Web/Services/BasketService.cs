﻿using FreeEducation.Shared.Dtos;
using FreeEducation.Web.Models.Baskets;
using FreeEducation.Web.Services.Interfaces;

namespace FreeEducation.Web.Services;

public class BasketService:IBasketService
{
    private readonly HttpClient _httpClient;
    private readonly IDiscountService _discountService;

    public BasketService(HttpClient httpClient, IDiscountService discountService)
    {
        _httpClient = httpClient;
        _discountService = discountService;
    }

    public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
    {
        var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("baskets",basketViewModel);

        return response.IsSuccessStatusCode;
    }

    public async Task<BasketViewModel> Get()
    {
        var response = await _httpClient.GetAsync("baskets");
        if (!response.IsSuccessStatusCode) return null;
        var basketViewModel = await response.Content.ReadFromJsonAsync<ResponseDto<BasketViewModel>>();
        return basketViewModel.Data;

    }

    public async Task<bool> Delete()
    {
        var result = await _httpClient.DeleteAsync("baskets");
        return result.IsSuccessStatusCode;
    }

    public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
    {
        var basket = await Get();
        if (basket != null)
        {
            if (!basket.BasketItems.Any(x => x.EducationId == basketItemViewModel.EducationId))
            {
                basket.BasketItems.Add(basketItemViewModel);
            }
        }
        else
        {
            basket = new BasketViewModel();
            basket.BasketItems.Add(basketItemViewModel);
        }

        await SaveOrUpdate(basket);
    }

    public async Task<bool> RemoveBasketItem(string educationId)
    {
        var basket = await Get();
        
        if (basket == null) return false;

        var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.EducationId == educationId);

        if (deleteBasketItem == null) return false;
        
        var deleteResult = basket.BasketItems.Remove(deleteBasketItem);

        if (!deleteResult) return false;

        if (!basket.BasketItems.Any()) basket.DiscountCode = null;

        return await SaveOrUpdate(basket);
        
    }

    public async Task<bool> ApplyDiscount(string discountCode)
    {
        await CancelApplyDiscount();
        var basket = await Get();
        if (basket == null) return false;
        var discount = await _discountService.GetDiscount(discountCode);
        if (discount == null) return false;
        basket.DiscountCode = discount.Code;
        basket.DiscountRate = discount.Rate;
        return await SaveOrUpdate(basket);

    }

    public async Task<bool> CancelApplyDiscount()
    {
        var basket = await Get();
        if (basket == null && basket.DiscountCode== null) return false;
        basket.CancelDiscount();
        return await SaveOrUpdate(basket);
    }
}