using CoursesManagament.Attributes;
using CoursesManagament.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagament.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
       
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  
        public bool RememberMe { get; set; }
        public Trainee? Trainee {  get; set; }
        public Trainer? Trainer { get; set; }


    }
}
