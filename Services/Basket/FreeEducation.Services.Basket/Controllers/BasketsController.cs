using FreeEducation.Services.Basket.Dtos;
using FreeEducation.Services.Basket.Services;
using FreeEducation.Shared.ControllerBases;
using FreeEducation.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeEducation.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService) : CustomControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            return CreateActionResultInstance(await basketService.GetBasket(sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basketDto)
        {
            basketDto.UserId = sharedIdentityService.GetUserId;
            return CreateActionResultInstance(await basketService.SaveOrUpdate(basketDto));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResultInstance(await basketService.Delete(sharedIdentityService.GetUserId));
        }
    }
}
