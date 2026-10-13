using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Model.Dto;

public class CreateCategory
{
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
    [StringLength(1000, ErrorMessage = "Description must be at most 1000 characters long.")]
    public string? Description { get; set; }
}
