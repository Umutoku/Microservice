using FreeEducation.Web.Models;
using FreeEducation.Web.Models.Catalogs;
using FreeEducation.Web.Services.Interfaces;

namespace FreeEducation.Web.Services;

public class CatalogService: ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<List<EducationViewModel>> GetAllEducationAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<CategoryViewModel>> GetAllCategoryAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<EducationViewModel>> GetAllEducationByUserIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteEducationAsync(string educationId)
    {
        throw new NotImplementedException();
    }

    public Task<EducationViewModel> GetByEducationId(string educationId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CreateEducationAsync(EducationCreateInput educationCreateInput)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateEducationAsync(EducationUpdateInput educationUpdateInput)
    {
        throw new NotImplementedException();
    }
}