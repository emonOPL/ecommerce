using System;
using ecommerce.Model;
using ecommerce.Model.Dto;

namespace ecommerce.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private readonly List<Category> _categories = new List<Category>();

    public CategoryRepository(List<Category> categories)
    {
        _categories = categories;
    }

    public List<Category> GetCategories(String? searchValue)
    {
        if (!string.IsNullOrEmpty(searchValue))
        {
            var searchResult = _categories.Where(c => c.Name.Contains(searchValue.Trim(), StringComparison.OrdinalIgnoreCase));
            return searchResult.ToList();
        }
        return _categories;
    }
    
    public Category? GetCategory(Guid id)
    {
        return _categories.FirstOrDefault(c => c.Id == id);
    }

    public Category CreateCategory(CreateCategory createCategory)
    {
        var category = new Category();
        category.Id = Guid.NewGuid();
        category.Name = createCategory.Name.Trim();
        category.Description = createCategory.Description?.Trim();
        category.CreatedAt = DateTime.UtcNow;
        _categories.Add(category);
        return category;
    }

    public Category? UpdateCategory(Guid id, CreateCategory updateCategory)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return null;
        }
        category.Name = updateCategory.Name.Trim();
        category.Description = updateCategory.Description?.Trim();
        return category;
    }

    public Category? PartialUpdateCategory(Guid id, PartialUpdateCategory partialUpdateCategory)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return null;
        }
        category.Name = partialUpdateCategory.Name?.Trim() ?? category.Name;
        category.Description = partialUpdateCategory.Description?.Trim() ?? category.Description;
        return category;
    }

    public bool DeleteCategory(Guid id)
    {
        var category = _categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return false;
        }
        _categories.Remove(category);
        return true;
    }

}
