using CoursesManagament.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManagament.Models;

public partial class Course
{
    public int Id { get; set; }

    [Required]
    [UniqueName]
    public string Name { get; set; } = null!;

    public string? Descripition { get; set; }

    public int CategoryId { get; set; }

    public int TrainerId { get; set; }

    public string? ImageId { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    [NotMapped]
    public string? query { get; set; }=null;
    [Required]
    public decimal Price { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Units> Units { get; set; } = new List<Units>();

    public virtual ICollection<TraineeCourse> TraineeCourses { get; set; } = new List<TraineeCourse>();

    public virtual Trainer? Trainer { get; set; }
    [NotMapped]
    public string? CurrentName {  get; set; }
    [NotMapped]
    public int? CurrentTrainerId { get; set;}
}





