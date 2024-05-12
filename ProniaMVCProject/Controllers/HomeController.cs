using Microsoft.AspNetCore.Mvc;
using ProniaMVCProject.Business.Services.Abstracts;
using ProniaMVCProject.Core.Models;
using ProniaMVCProject.Data.DAL;
using ProniaMVCProject.ViewModels;
using System.Diagnostics;

namespace ProniaMVCProject.Controllers;

public class HomeController : Controller
{
    private readonly IFeatureService _featureService;
    private readonly ISliderService _sliderService;

    public HomeController(IFeatureService featureService, ISliderService sliderService)
    {
        _featureService = featureService;
        _sliderService = sliderService;
    }

    public IActionResult Index()
    {
        var features = _featureService.GetAllFeatures();
        var sliders = _sliderService.GetAllSliders();

        HomeVM homeVM = new HomeVM()
        {
            Features = features,
            Sliders = sliders
        };

        return View(homeVM);
    }

}
