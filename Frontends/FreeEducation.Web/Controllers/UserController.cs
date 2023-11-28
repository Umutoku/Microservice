using FreeEducation.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeEducation.Web.Controllers;
[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        return View(await _userService.GetUser());
    }
}