using System.Net;
using FreeEducation.Shared.Dtos;
using FreeEducation.Web.Helpers;
using FreeEducation.Web.Models.Catalogs;
using FreeEducation.Web.Services.Interfaces;

namespace FreeEducation.Web.Services;

public class CatalogService(HttpClient httpClient, IPhotoStockService photoStockService,PhotoHelper photoHelper) : ICatalogService
{

    public async Task<List<EducationViewModel>> GetAllEducationAsync()
    {
        var response = await httpClient.GetAsync("Educations");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<EducationViewModel>>>();

        foreach (var educationViewModel in responseSuccess.Data)
        {
            educationViewModel.StockPictureUrl = photoHelper.GetPhotoStockUrl(educationViewModel.Picture);
        }

        return responseSuccess.Data;
    }

    public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
    {
        var response = await httpClient.GetAsync("Categories");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<CategoryViewModel>>>();
        return responseSuccess.Data;
    }

    public async Task<List<EducationViewModel>> GetAllEducationByUserIdAsync(string userId)
    {
        var response = await httpClient.GetAsync($"educations/GetAllByUserId/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<List<EducationViewModel>>>();

        foreach (var educationViewModel in responseSuccess.Data)
        {
            educationViewModel.StockPictureUrl = photoHelper.GetPhotoStockUrl(educationViewModel.Picture);
        }

        return responseSuccess.Data;
    }

    public async Task<bool> DeleteEducationAsync(string educationId)
    {
        var response = await httpClient.DeleteAsync($"educations/{educationId}");

        return response.IsSuccessStatusCode;
    }

    public async Task<EducationViewModel> GetByEducationId(string educationId)
    {
        var response = await httpClient.GetAsync($"educations/{educationId}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseSuccess = await response.Content.ReadFromJsonAsync<ResponseDto<EducationViewModel>>();
        responseSuccess.Data.StockPictureUrl = photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);
        return responseSuccess.Data;
    }

    public async Task<bool> CreateEducationAsync(EducationCreateInput educationCreateInput)
    {
        var resultPhotoService = await photoStockService.UploadPhoto(educationCreateInput.PhotoFormFile);
        if (resultPhotoService != null)
        {
            educationCreateInput.Picture = resultPhotoService.Url;
        }
        var response = await httpClient.PostAsJsonAsync<EducationCreateInput>("educations", educationCreateInput);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateEducationAsync(EducationUpdateInput educationUpdateInput)
    {
        var resultPhotoService = await photoStockService.UploadPhoto(educationUpdateInput.PhotoFormFile);
        if (resultPhotoService != null)
        {
            await photoStockService.DeletePhoto(educationUpdateInput.Picture);
            educationUpdateInput.Picture = resultPhotoService.Url;
        }

        var response = await httpClient.PutAsJsonAsync<EducationUpdateInput>("educations", educationUpdateInput);

        return response.IsSuccessStatusCode;
    }
}