using CoursesManagament.Models;
using CoursesManagament.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Security.Claims;

namespace CoursesManagament.Controllers
{

    public class CourseController : Controller
    {
        ICourseRepository courseRepository;
        ICategoryRepoistory categoryRepository;
        ITrainerRepository trainerRepository;
        IUnitRepository unitRepository;
        private readonly IWebHostEnvironment _environment;

        public CourseController(ICourseRepository courseRepository,ICategoryRepoistory categoryRepoistory,ITrainerRepository trainerRepository ,IUnitRepository unitRepository)
        {
            this.courseRepository = courseRepository;
            this.categoryRepository=categoryRepoistory;
            this.trainerRepository=trainerRepository;
            this.unitRepository=unitRepository;
           
        }
        [Authorize(Roles = "Admin,Trainer")]
        public IActionResult Index( int? id = null,string query=null,int? categoryid=null)
            {
            var roleclaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            ViewBag.Role = roleclaim?.Value;
            var trainerclaim = User.Claims.FirstOrDefault(c => c.Type == "TrainerId");
          
            if (ViewBag.Role == "Trainer")
            {
                if(int.TryParse(trainerclaim?.Value,out int trainerid))
                id = trainerid;
            }
            var courses = courseRepository.GetAll(categoryid, query, id);
            ViewBag.Categories = categoryRepository.GetAll();
            ViewBag.Trainers = trainerRepository.GetAll();

            return View(courses);
        }
        [Authorize(Roles = "Admin,Trainer")]
        public IActionResult Add()
        {
        
            var roleclaim=User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.Role);
            var trainerid = User.Claims.FirstOrDefault(c => c.Type == "TrainerId");
            ViewBag.Role = roleclaim?.Value;
            ViewBag.trainerid=trainerid?.Value;
            ViewBag.Categories = categoryRepository.GetAll();
            ViewBag.Trainers = trainerRepository.GetAll();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Trainer")]
        public IActionResult Add(Course course) {

          


            if (ModelState.IsValid == true)
            {
             
                courseRepository.Create(course);
                 
                
                    return RedirectToAction("Index");

                

            }
            var roleclaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            ViewBag.Role = roleclaim?.Value;
            var trainerid = User.Claims.FirstOrDefault(c => c.Type == "TrainerId");
            ViewBag.trainerid = trainerid?.Value;

            ViewBag.Categories = categoryRepository.GetAll();
            ViewBag.Trainers = trainerRepository.GetAll();

            return View(course);
        }
        [Authorize(Roles = "Admin,Trainer")]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories=categoryRepository.GetAll();
            ViewBag.Trainers=trainerRepository.GetAll();
            var _course = courseRepository.GetById(id);
            var roleclaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            ViewBag.Role = roleclaim?   .Value;
            var trainerid = User.Claims.FirstOrDefault(c => c.Type == "TrainerId");
            ViewBag.trainerid = trainerid?.Value;
            _course.CurrentName=_course.Name;
            _course.CurrentTrainerId=_course.TrainerId;

            return View(_course);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Trainer")]
        public IActionResult Edit(Course course,int id)
        {
            if (ModelState.IsValid == true)
            {
                courseRepository.Update(course, id);
                return RedirectToAction("Index");
            }
            var roleclaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            ViewBag.Role = roleclaim?.Value;
            var trainerid = User.Claims.FirstOrDefault(c => c.Type == "TrainerId");
            ViewBag.trainerid = trainerid?.Value;
            ViewBag.Categories = categoryRepository.GetAll();
            ViewBag.Trainers = trainerRepository.GetAll();
            var ExistCourse = courseRepository.GetById(id);
            course.CurrentName = ExistCourse.Name;
            course.CurrentTrainerId=ExistCourse.TrainerId;


            return View(course);
        }
        [Authorize(Roles = "Admin,Trainer")]
        public IActionResult Delete(int id)
        {
          
            courseRepository.Delete(id);
            
           
                return RedirectToAction("Index");

           

        }
        [AllowAnonymous]
        public IActionResult Default()
        {

            var courses =  courseRepository.GetAll(null,null,null);
            return View(courses);
        }
        [Authorize(Roles ="Trainee")]

        public IActionResult Info(int id)
        {

            var course = courseRepository.GetById(id);
            ViewBag.Units=unitRepository.GetCourseUnits(id);
            ViewBag.msg = TempData["msg"];
            return View(course);
        }

        [Authorize(Roles = "Trainee")]

       public IActionResult Subscribe(int id , int TraineeId) {
           var claim= User.Claims.FirstOrDefault(c=>c.Type=="TraineeId");
            
            if (int.TryParse(claim.Value, out int number))
            {
                TraineeId = number;
                if(courseRepository.AddTraineeToCourse(id, TraineeId))
                {
                   TempData["Msg"] = "subscribed successfully";

                }
                else
                {
                    TempData["Msg"] = "you can't because you already Subscribed in this course";

                }
            }
            return RedirectToAction("Info", new {id=id});
        }

        
    }
}
