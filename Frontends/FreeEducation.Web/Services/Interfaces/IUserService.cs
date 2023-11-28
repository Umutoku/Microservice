using FreeEducation.Web.Models;

namespace FreeEducation.Web.Services.Interfaces;

public interface IUserService
{
    Task<UserViewModel> GetUser();
    
}