using System;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Model;

public class Category
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = String.Empty;
    [StringLength(1000, ErrorMessage = "Description must be at most 1000 characters long.")]
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
