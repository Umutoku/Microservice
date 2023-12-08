using FreeEducation.Web.Models;
using FreeEducation.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FreeEducation.Web.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace FreeEducation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICatalogService _catalogService;

        public HomeController(ILogger<HomeController> logger, ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Detail(string id)
        {
            return View(await _catalogService.GetByEducationId(id));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllEducationAsync());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (errorFeature != null && errorFeature.Error is UnAuthorizeException)
            {
                return RedirectToAction(nameof(AuthController.Logout), "Auth");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
