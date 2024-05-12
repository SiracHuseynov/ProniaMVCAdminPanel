using Microsoft.AspNetCore.Mvc;
using ProniaMVCProject.Business.Exceptions;
using ProniaMVCProject.Business.Services.Abstracts;
using ProniaMVCProject.Core.Models;

namespace ProniaMVCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var sliders = _sliderService.GetAllSliders();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (slider == null) return NotFound();

            try
            {
                await _sliderService.AddSlider(slider);
            }
            catch (ImageContextException ex)
            {
                ModelState.AddModelError("slider.ImageFile.ContentType", ex.Message);
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("slider.ImageFile.Length", ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var slider = _sliderService.GetSlider(x => x.Id == id);

            if (slider == null) return NotFound();

            return View(slider);
        }


        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            if ((!ModelState.IsValid))
            {
                return View();
            }

            try
            {
                _sliderService.UpdateSlider(slider.Id, slider);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (ImageContextException ex)
            {
                ModelState.AddModelError("slider.ImageFile.ContentType", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("slider.ImageFile.Length", ex.Message);
                return View();
            }

            return RedirectToAction("index");

        }

        public IActionResult Delete(int id)
        {
            var slider = _sliderService.GetSlider(x => x.Id == id);

            if (slider == null) return NotFound();

            return View(slider);

        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            try
            {
                _sliderService.DeleteSlider(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }



    }
}
