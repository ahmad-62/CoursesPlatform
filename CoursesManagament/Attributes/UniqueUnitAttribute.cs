using CoursesManagament.Models;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagament.Attributes
{
    public class UniqueUnitAttribute:ValidationAttribute

    {
        AppDbContext context = new AppDbContext();
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name=value.ToString();
            Units unit =(Units)validationContext.ObjectInstance;
            var _isExist=context.Units.FirstOrDefault(u => u.Name==name&&u.CourseId==unit.CourseId);
            if (_isExist==null)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("This unit is already Found");  
        }
    }
}
