using FreeEducation.Services.Catalog.Dtos;
using FreeEducation.Services.Catalog.Services;
using FreeEducation.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeEducation.Services.Catalog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : CustomControllerBase
    {
        private readonly IEducationService _educationService;

        public EducationsController(IEducationService educationService)
        {
            _educationService = educationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _educationService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _educationService.GetByIdAsync(id);
            return CreateActionResultInstance(response);
        }

        [HttpGet("ByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _educationService.GetAllByUserIdAsync(userId);
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EducationCreateDto educationDto)
        {
            var response = await _educationService.CreateAsync(educationDto);
            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(EducationUpdateDto educationUpdateDto)
        {
            var response = await _educationService.UpdateAsync(educationUpdateDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _educationService.DeleteAsync(id);
            return CreateActionResultInstance(response);
        }

    }
}
