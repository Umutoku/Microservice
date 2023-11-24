using FreeEducation.Services.Discount.Services;
using FreeEducation.Shared.ControllerBases;
using FreeEducation.Shared.Dtos;
using FreeEducation.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeEducation.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController(IDiscountService discountService, ISharedIdentityService sharedIdentityService) : CustomControllerBase
    {
        private readonly IDiscountService _discountService = discountService;
        private readonly ISharedIdentityService _sharedIdentityService = sharedIdentityService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discounts = await _discountService.GetAll();
            return CreateActionResultInstance(discounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discount = await _discountService.GetById(id);
            return CreateActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Models.Discount discount)
        {
            var response = await _discountService.Save(discount);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            var response = await _discountService.Update(discount);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _discountService.Delete(id);
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId;
            var response = await _discountService.GetByCodeAndUserId(code, userId);
            return CreateActionResultInstance(response);
        }
    }
}
