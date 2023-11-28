using FreeEducation.Web.Models;
using FreeEducation.Web.Services.Interfaces;

namespace FreeEducation.Web.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<UserViewModel> GetUser()
    {
        return _httpClient.GetFromJsonAsync<UserViewModel>("/api/User/GetUser");
    }
}