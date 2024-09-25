using CoursesManagament.Models;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagament.Attributes
{
    public class UniqueLessonAttribute:ValidationAttribute
    {
        AppDbContext context = new AppDbContext();
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name=value.ToString();
            var lesson=(Lesson)validationContext.ObjectInstance;
            var _isexist=context.Lessons.FirstOrDefault(x=>x.Title==name&&x.UnitId==lesson.UnitId);
            if (_isexist==null) {
                return ValidationResult.Success;
            }
            return new ValidationResult("This Lesson is already Found");

        }

    }
}
