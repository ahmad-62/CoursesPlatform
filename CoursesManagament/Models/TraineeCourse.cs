using System;
using System.Collections.Generic;

namespace CoursesManagament.Models;

public partial class TraineeCourse
{
    public int TraineeId { get; set; }

    public int CourseId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Trainee Trainee { get; set; } = null!;
}
