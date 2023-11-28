using FreeEducation.Shared.Dtos;
using FreeEducation.Web.Models;
using IdentityModel.Client;

namespace FreeEducation.Web.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<ResponseDto<bool>> SignIn(SignInInput signInInput);

        Task<TokenResponse> GetAccessTokenByRefreshToken();

        Task RevokeRefreshToken();
    }
}
