using ProniaMVCProject.Business.Exceptions;
using ProniaMVCProject.Business.Services.Abstracts;
using ProniaMVCProject.Core.Models;
using ProniaMVCProject.Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Business.Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task AddCategory(Category category)
        {
            if (!_categoryRepository.GetAll().Any(x => x.Name == category.Name))
            {
                await _categoryRepository.AddAsync(category);
                await _categoryRepository.CommitAsync();
            }
            else
            {
                throw new DuplicateException("Category adi eyni ola bilmez!");
            }
        }

        public void DeleteCategory(int id)
        {
            Category category = _categoryRepository.Get(x => x.Id == id);

            if (category == null) throw new EntityNotFoundException("Bele bir category movcud deyil");

            _categoryRepository.Delete(category);
            _categoryRepository.Commit();
        }

        public List<Category> GetAllCategories(Func<Category, bool>? predicate = null)
        {
            return _categoryRepository.GetAll(predicate);
        }

        public Category GetCategory(Func<Category, bool>? predicate = null)
        {
            return _categoryRepository.Get(predicate);
        }

        public void UpdateCategory(int id, Category newCategory)
        {
            Category category = _categoryRepository.Get(x => x.Id == id );

            if (category == null) throw new EntityNotFoundException("Bele bir category movcud deyil");

            if (!_categoryRepository.GetAll().Any(x => x.Name == newCategory.Name &&  x.Id != category.Id))
            {
                category.Name = newCategory.Name;
            } 
            else
            {
                throw new DuplicateException("Category adi eyni ola bilmez!");
            }
            _categoryRepository.Commit();
        }
    }
}
