using CoursesManagament.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoursesManagament.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
     
      

    }
}
/*










using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagament.Models;

public partial class Trainer
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [EmailAddress(ErrorMessage ="Write The Email in correct form")]
    [Remote("CheckEmail","Treainer",ErrorMessage =("This Email is already found"))]
    public string? Email { get; set; }
    [StringLength(250,MinimumLength =10)]
    public string? Descripition { get; set; }
    [Display(Name="Trainer")]
    public String Name => FirstName + " " + LastName;

    public DateTime CreationDate { get; set; }
    [Url(ErrorMessage ="Write the url in correct form")]
    public string? WebSite { get; set; }

    public virtual AspNetUser? AspNetUser { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}






using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagament.Models;

public partial class Course
{

    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;

    public string? Descripition { get; set; }

    public int CategoryId { get; set; }

    public int TrainerId { get; set; }

    public virtual Category? Category { get; set; } 

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<TraineeCourse> TraineeCourses { get; set; } = new List<TraineeCourse>();

    public virtual Trainer? Trainer { get; set; } 
}
 
 
 
 
 
 */