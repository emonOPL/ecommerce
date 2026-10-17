using System;
using ecommerce.Model;
using ecommerce.Model.Dto;

namespace ecommerce.Repositories;

public interface ICategoryRepository
{
    public Category? GetCategory(Guid id);
    public List<Category> GetCategories(string? searchValue);
    public Category CreateCategory(CreateCategory createCategory);
    public Category UpdateCategory(Guid id, CreateCategory updateCategory);
    public Category PartialUpdateCategory(Guid id, CreateCategory updateCategory);
    public void DeleteCategory(Guid id);
}
