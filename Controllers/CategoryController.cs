using System;
using ecommerce.Model;
using ecommerce.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers;

[ApiController]
[Route("api/categories")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class CategoryController: ControllerBase
{
    public static List<Category> categories = new List<Category>(); 

    [HttpGet]
    public IActionResult GetCategories(string? searchValue)
    {
        if (!string.IsNullOrEmpty(searchValue))
        {
            var searchResult = categories.Where(c => c.Name.Contains(searchValue.Trim(), StringComparison.OrdinalIgnoreCase));
            return Ok(searchResult);
        }
        return Ok(categories);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetCategory([FromRoute] Guid id)
    {
        Category? category = categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CreateCategory createCategory)
    {
        Category category = new Category();
        category.Id = Guid.NewGuid();
        category.Name = createCategory.Name.Trim();
        category.Description = createCategory.Description?.Trim();
        category.CreatedAt = DateTime.UtcNow;
        categories.Add(category);
        return Created($"/api/categories/{category.Id}", category);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateCategory([FromRoute] Guid id, [FromBody] CreateCategory updateCategory)
    {
        Category? category = categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        category.Name = updateCategory.Name.Trim();
        category.Description = updateCategory.Description?.Trim();
        return NoContent();
    }

    [HttpPatch]
    [Route("{id:guid}")]
    public IActionResult PartialUpdateCategory([FromRoute] Guid id, [FromBody] PartialUpdateCategory partialUpdateCategory)
    {
        var name = partialUpdateCategory?.Name?.Trim();         
        if (name == "")                                 
        {
            return BadRequest("Name cannot be empty.");
        }
        if (partialUpdateCategory?.Description?.Trim().Length > 1000)
        {
            return BadRequest();
        }
        Category? category = categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        category.Name = name ?? category.Name;
        category.Description = partialUpdateCategory?.Description?.Trim() ?? category.Description;
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteCategory([FromRoute] Guid id)
    {
        Category? category = categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        categories.Remove(category);
        return NoContent();
    }

}
