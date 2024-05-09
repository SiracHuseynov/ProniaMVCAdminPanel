using Microsoft.AspNetCore.Mvc;
using ProniaMVCProject.Business.Services.Abstracts;
using ProniaMVCProject.Core.Models;

namespace ProniaMVCProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService CategoryCategory)
        {
            _categoryService = CategoryCategory;
        }
        public IActionResult Index()
        {
            var category = _categoryService.GetAllCategories();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category Category)
        {
            await _categoryService.AddCategory(Category);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetCategory(x => x.Id == id);
            if (category == null)
            {
                throw new NullReferenceException("Bele bir Model yoxdur!!");
            }
            else
            {
                _categoryService.DeleteCategory(id);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Update(int id)
        {
            var category = _categoryService.GetCategory(x => x.Id == id);
            if (category == null)
            {
                throw new NullReferenceException("Bele bir User yoxdur!!");
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category newCategory)
        {
            _categoryService.UpdateCategory(newCategory.Id, newCategory);
            return RedirectToAction("Index");
        }
    }
}
