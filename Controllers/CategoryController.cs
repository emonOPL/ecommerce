using System;
using ecommerce.Model;
using ecommerce.Model.Dto;
using ecommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Controllers;

[ApiController]
[Route("api/categories")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class CategoryController: ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult GetCategories(string? searchValue)
    {
        return Ok(_categoryService.GetCategories(searchValue));
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetCategory([FromRoute] Guid id)
    {
        Category? category = _categoryService.GetCategory(id);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CreateCategory createCategory)
    {
        var category = _categoryService.CreateCategory(createCategory);
        return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult UpdateCategory([FromRoute] Guid id, [FromBody] CreateCategory updateCategory)
    {
        Category? category = _categoryService.UpdateCategory(id, updateCategory);
        if (category == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPatch]
    [Route("{id:guid}")]
    public IActionResult PartialUpdateCategory([FromRoute] Guid id, [FromBody] PartialUpdateCategory partialUpdateCategory)
    {
        var name = partialUpdateCategory.Name?.Trim();         
        if (name == "")                                 
        {
            return BadRequest("Name cannot be empty.");
        }
        if (partialUpdateCategory.Description?.Trim().Length > 1000)
        {
            return BadRequest();
        }
        Category? category = _categoryService.PartialUpdateCategory(id, partialUpdateCategory);
        if (category == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public IActionResult DeleteCategory([FromRoute] Guid id)
    {
        bool isDeleted = _categoryService.DeleteCategory(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }

}
