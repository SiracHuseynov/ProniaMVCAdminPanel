using Microsoft.AspNetCore.Mvc;
using ProniaMVCProject.Business.Services.Abstracts;
using System.Diagnostics;

namespace ProniaMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeatureService _featureService;

        public HomeController(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public IActionResult Index()
        {
            var features = _featureService.GetAllFeatures();
            return View(features);
        }

    }
}
