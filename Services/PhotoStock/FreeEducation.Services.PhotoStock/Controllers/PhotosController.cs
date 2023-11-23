using FreeEducation.Services.PhotoStock.Dtos;
using FreeEducation.Shared.ControllerBases;
using FreeEducation.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeEducation.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo,CancellationToken cancellationToken)
        {
            if (photo is null || photo.Length <= 0)
            {
                return CreateActionResultInstance(ResponseDto<PhotoDto>.Fail("Photo is not found", 400));
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream,cancellationToken);

            var returnPath= "photos/" + photo.FileName;

            PhotoDto photoDto = new() { Url = returnPath };

            return CreateActionResultInstance(ResponseDto<PhotoDto>.Success(photoDto, 200));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return CreateActionResultInstance(ResponseDto<NoContent>.Success(204));
            }
            return CreateActionResultInstance(ResponseDto<NoContent>.Fail("Photo is not found", 404));
        }
    }
}
