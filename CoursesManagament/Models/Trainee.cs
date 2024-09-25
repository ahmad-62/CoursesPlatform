using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManagament.Models;

public partial class Trainee
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public int? IsActive { get; set; }


    public virtual ApplicationUser User { get; set; }

    public virtual ICollection<TraineeCourse> TraineeCourses { get; set; } = new List<TraineeCourse>();
}
