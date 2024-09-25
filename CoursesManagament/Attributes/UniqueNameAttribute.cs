using CoursesManagament.Models;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagament.Attributes
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        AppDbContext context = new AppDbContext();

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var _course = value?.ToString();
            var course = (Course)validationContext.ObjectInstance;
            var existCourse = context.Courses.FirstOrDefault(x => x.Name == _course && x.TrainerId == course.TrainerId);
            if (existCourse == null||(existCourse.Name== course.CurrentName&&existCourse.TrainerId == course.CurrentTrainerId))
            {
                return ValidationResult.Success;

            }
            return new ValidationResult("This course already found with this trainer");
        }
    }
}
