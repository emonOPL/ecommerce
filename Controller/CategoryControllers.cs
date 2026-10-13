using System;
using ecommerce.Model;
using ecommerce.Model.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers;

[Route("api/categories/")]
public class CategoryControllers: Controller
{
    public static List<Category> categories = new List<Category>(); 

    [HttpGet]
    [Route("")]
    public IActionResult GetCategories(string? searchValue)
    {
        if (!string.IsNullOrEmpty(searchValue))
        {
            var searchResult = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
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
    [Route("")]
    public IActionResult CreateCategory([FromBody] CreateCategory createCategory)
    {
        if(ModelState.ErrorCount > 0)
        {
            return BadRequest(ModelState);
        }
        Category category = new Category();
        category.Id = Guid.NewGuid();
        category.Name = createCategory.Name;
        category.Description = createCategory.Description;
        category.CreatedAt = DateTime.Now;
        categories.Add(category);
        return Created($"/api/categories/{category.Id}", category);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateCategory([FromRoute] Guid id, [FromBody] CreateCategory updateCategory)
    {
        if(ModelState.ErrorCount > 0)
        {
            return BadRequest(ModelState);
        }
        Category? category = categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        category.Name = updateCategory.Name;
        category.Description = updateCategory.Description;
        return NoContent();
    }

    [HttpPatch]
    [Route("{id:guid}")]
    public IActionResult PartialUpdateCategory([FromRoute] Guid id, [FromBody] CreateCategory updateCategory)
    {
        if (updateCategory?.Description?.Length > 1000)
        {
            return BadRequest();
        }
        Category? category = categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound();
        }
        var name = updateCategory?.Name?.Trim();         
        if (name == "")                                 
        {
            return BadRequest("Name cannot be empty.");
        }
        category.Name = name ?? category.Name;
        category.Description = updateCategory?.Description ?? category.Description;
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
