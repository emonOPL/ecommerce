using System;
using ecommerce.Model;
using ecommerce.Model.Dto;

namespace ecommerce.Repositories;

public class CategoryRepository: ICategoryRepository
{
    private readonly List<Category> _categories = new List<Category>();

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

    public Category CreateCategory(Category category)
    {
        _categories.Add(category);
        return category;
    }

    public Category UpdateCategory(Category category)
    {
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
