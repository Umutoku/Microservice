using FreeEducation.Shared.Services;
using FreeEducation.Web.Models.Catalogs;
using FreeEducation.Web.Services.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeEducation.Web.Controllers;
[Authorize]
public class EducationsController : Controller
{
    private readonly ICatalogService _catalogService;

    private readonly ISharedIdentityService _identityService;

    public EducationsController(ICatalogService catalogService, ISharedIdentityService identityService)
    {
        _catalogService = catalogService;
        _identityService = identityService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _catalogService.GetAllEducationByUserIdAsync(_identityService.GetUserId));
    }

    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetAllCategoryAsync();
        ViewBag.categoryList = new SelectList(categories,"Id","Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(EducationCreateInput educationCreateInput)
    {
        //educationCreateInput.UserId = _identityService.GetUserId;
        ViewBag.UserId = _identityService.GetUserId;
        var categories = await _catalogService.GetAllCategoryAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        if (!ModelState.IsValid)
        {
            return View();
        }

        var response = await _catalogService.CreateEducationAsync(educationCreateInput);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(string id)
    {
        ViewBag.UserId = _identityService.GetUserId;
        var education = await _catalogService.GetByEducationId(id);
        var categories = await _catalogService.GetAllCategoryAsync();

        if (education == null)
        {
            RedirectToAction(nameof(Index));
        }
        ViewBag.categoryList = new SelectList(categories, "Id", "Name",education.Id);
        EducationUpdateInput educationUpdateInput = new()
        {
            Id = education.Id,
            Name = education.Name,
            Price = education.Price,
            Feature = education.Feature,
            CategoryId = education.CategoryId,
            UserId = education.UserId,
            //Picture = education.Picture,
            Description = education.Description

        };
        return View(educationUpdateInput);
    }
    [HttpPost]
    public async Task<IActionResult> Update(EducationUpdateInput educationUpdateInput)
    {
        var categories = await _catalogService.GetAllCategoryAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name",educationUpdateInput.Id);
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _catalogService.UpdateEducationAsync(educationUpdateInput);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        await _catalogService.DeleteEducationAsync(id);
        return RedirectToAction(nameof(Index));
    }
}