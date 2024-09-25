using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagament.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName {  get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote(action:"CheckEmail", controller:"Account",ErrorMessage ="This Email is already use")]
        public string Email { get; set; }
        [Required]
        public string UserName {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        

    }
}
