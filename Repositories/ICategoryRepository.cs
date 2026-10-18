using System;
using ecommerce.Model;
using ecommerce.Model.Dto;

namespace ecommerce.Repositories;

public interface ICategoryRepository
{
    public Category? GetCategory(Guid id);
    public List<Category> GetCategories(string? searchValue);
    public Category CreateCategory(Category category);
    public Category UpdateCategory(Category category);
    public bool DeleteCategory(Guid id);
}
