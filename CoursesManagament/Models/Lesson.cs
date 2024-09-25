using CoursesManagament.Attributes;
using System;
using System.Collections.Generic;

namespace CoursesManagament.Models;

public partial class Lesson
{
    public int Id { get; set; }
    [UniqueLesson]
    public string? Title { get; set; } = null!;

    public int UnitId { get; set; }

    public int? OrderNumber { get; set; }

    public string? Type {  get; set; } 

    public virtual Units? Units { get; set; } = null!;
   
}
