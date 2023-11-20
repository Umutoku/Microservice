using FreeEducation.Services.Catalog.Dtos;
using FreeEducation.Shared.Dtos;

namespace FreeEducation.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);

    }
}
