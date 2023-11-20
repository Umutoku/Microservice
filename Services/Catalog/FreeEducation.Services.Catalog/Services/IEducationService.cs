using FreeEducation.Services.Catalog.Dtos;
using FreeEducation.Shared.Dtos;

namespace FreeEducation.Services.Catalog.Services
{
    public interface IEducationService
    {
        Task<ResponseDto<List<EducationDto>>> GetAllAsync();
        Task<ResponseDto<EducationDto>> CreateAsync(EducationCreateDto educationDto);
        Task<ResponseDto<EducationDto>> GetByIdAsync(string id);
        Task<ResponseDto<List<EducationDto>>> GetAllByUserIdAsync(string userId);
        Task<ResponseDto<NoContent>> UpdateAsync(EducationUpdateDto educationUpdateDto);
        Task<ResponseDto<NoContent>> DeleteAsync(string id);
    }
}
