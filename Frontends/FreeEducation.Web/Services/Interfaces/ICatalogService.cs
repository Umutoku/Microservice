using FreeEducation.Web.Models.Catalogs;

namespace FreeEducation.Web.Services.Interfaces;

public interface ICatalogService
{
    Task<List<EducationViewModel>> GetAllEducationAsync();
    Task<List<CategoryViewModel>> GetAllCategoryAsync();
    Task<List<EducationViewModel>> GetAllEducationByUserIdAsync(string userId);
    Task<bool> DeleteEducationAsync(string educationId);
    Task<EducationViewModel> GetByEducationId(string educationId);
    Task<bool> CreateEducationAsync(EducationCreateInput educationCreateInput);
    Task<bool> UpdateEducationAsync(EducationUpdateInput educationUpdateInput);
}