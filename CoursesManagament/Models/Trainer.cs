using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManagament.Models;

public partial class Trainer
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress(ErrorMessage = "Write The Email in correct form")]
    [Remote("CheckEmail", "Account", ErrorMessage = ("This Email is already found"),AdditionalFields ="CurrentEmail")]
    public string? Email { get; set; }
    [StringLength(250, MinimumLength = 10)]
    public string? Descripition { get; set; }
    [Display(Name = "Trainer")]
    public String Name => FirstName + " " + LastName;
    [NotMapped]
    [Required]
    [DataType(DataType.Password)]
    public string Password {  get; set; }
    [Required]
    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 5)]

    public string username {  get; set; }
    [NotMapped]
    public string? CurrentEmail {  get; set; }

    public DateTime CreationDate { get; set; }
    [Url(ErrorMessage = "Write the url in correct form")]
    public string? WebSite { get; set; }


    public virtual ApplicationUser? User { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}