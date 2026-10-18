using System;
using ecommerce.Model;
using ecommerce.Model.Dto;
using ecommerce.Repositories;

namespace ecommerce.Services;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public List<Category> GetCategories(String? searchValue) => _categoryRepository.GetCategories(searchValue);

    public Category? GetCategory(Guid id) => _categoryRepository.GetCategory(id);

    public Category CreateCategory(CreateCategory createCategory) 
    {
        Category category = new Category();
        category.Id = Guid.NewGuid();
        category.Name = createCategory.Name.Trim();
        category.Description = createCategory.Description?.Trim();
        category.CreatedAt = DateTime.UtcNow;
        return _categoryRepository.CreateCategory(category);
    }

    public Category? UpdateCategory(Guid id, CreateCategory updateCategory) 
    { 
        var category = _categoryRepository.GetCategory(id);
        if (category == null)
        {
            return null;
        }
        category.Name = updateCategory.Name.Trim();
        category.Description = updateCategory.Description?.Trim();
        return _categoryRepository.UpdateCategory(category);
    }

    public Category? PartialUpdateCategory(Guid id, PartialUpdateCategory partialUpdateCategory)
    { 
        var category = _categoryRepository.GetCategory(id);
        if (category == null)
        {
            return null;
        }
        category.Name = partialUpdateCategory.Name?.Trim() ?? category.Name;
        category.Description = partialUpdateCategory.Description?.Trim() ?? category.Description;
        return _categoryRepository.UpdateCategory(category);
    }

    public bool DeleteCategory(Guid id) => _categoryRepository.DeleteCategory(id);

}
