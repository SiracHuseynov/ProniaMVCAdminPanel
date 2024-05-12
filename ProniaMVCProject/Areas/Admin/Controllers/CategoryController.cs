using Microsoft.AspNetCore.Mvc;
using ProniaMVCProject.Business.Exceptions;
using ProniaMVCProject.Business.Services.Abstracts;
using ProniaMVCProject.Business.Services.Concretes;
using ProniaMVCProject.Core.Models;

namespace ProniaMVCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var categories = _categoryService.GetAllCategories();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                await _categoryService.AddCategory(category);
            }
            catch (DuplicateException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var existCategory = _categoryService.GetCategory(x => x.Id == id);
            if (existCategory == null) throw new NullReferenceException();

            return View(existCategory);
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {

            if(!ModelState.IsValid)
                return View();  
            try
            {
                _categoryService.UpdateCategory(category.Id, category);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(DuplicateException ex)
            {
                ModelState.AddModelError("Name", ex.Message);
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            var existCategory = _categoryService.GetCategory(x => x.Id == id);

            if (existCategory == null)
            {
                return NotFound();
            }

            return View(existCategory);
        }

    }
}
