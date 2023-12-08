﻿using FreeEducation.Web.Models.Baskets;
using FreeEducation.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeEducation.Web.Controllers;
[Authorize]
public class BasketController(IBasketService basketService, ICatalogService catalogService)
    : Controller
{
    // GET
    public async Task<IActionResult> Index()
    {
        return View(await basketService.Get());
    }

    public async Task<IActionResult> AddBasketItem(string educationId)
    {
        var education = await catalogService.GetByEducationId(educationId);

        var basketItem = new BasketItemViewModel
        {
            EducationId = education.Id,
            EducationName = education.Name,
            Price = education.Price
        };
        await basketService.AddBasketItem(basketItem);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveBasketItem(string educationId)
    {
        await basketService.RemoveBasketItem(educationId);

        return RedirectToAction(nameof(Index));
    }
}