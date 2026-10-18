using System;
using ecommerce.Model;
using ecommerce.Model.Dto;

namespace ecommerce.Services;

public interface ICategoryService
{
    public List<Category> GetCategories(String? searchValue);

    public Category? GetCategory(Guid id);

    public Category CreateCategory(CreateCategory createCategory);

    public Category? UpdateCategory(Guid id, CreateCategory updateCategory);

    public Category? PartialUpdateCategory(Guid id, PartialUpdateCategory partialUpdateCategory);

    public bool DeleteCategory(Guid id);
}
