using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManagament.Models;

public partial class Category
{
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 4)]
    [Remote("CheckCategory", "Category", ErrorMessage = "This Category is already Found",AdditionalFields = "CurrentName")]
    public string Name { get; set; } = null!;

    public int? ParentId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    public virtual Category? Parent { get; set; }
    [NotMapped]
    public string? CurrentName {  get; set; }
}