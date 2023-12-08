using FreeEducation.Web.Models.PhotoStocks;

namespace FreeEducation.Web.Services.Interfaces;

public interface IPhotoStockService
{
    Task<PhotoViewModel> UploadPhoto(IFormFile formFile);

    Task<bool> DeletePhoto(string photoUrl);
}