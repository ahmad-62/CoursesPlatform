using CoursesManagament.Models;
using CoursesManagament.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursesManagament.Controllers
{
    [Authorize(Roles ="Admin,Trainer")]
    public class UnitController : Controller
    {
        private readonly IUnitRepository unitRepository ;
        public UnitController(IUnitRepository unitRepository) {
        this.unitRepository = unitRepository ;
        
        }
        public IActionResult Index(int CourseId)
        {
            
            var units=unitRepository.GetCourseUnits(CourseId);
            ViewBag.CourseId=CourseId;

            return View(units);
        }
        public IActionResult Add(int CourseId)
        {
            ViewBag.Numbers = Enumerable.Range(1, 10).ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Add(Units unit,List<Lesson> lessons) {
            ViewBag.Numbers = Enumerable.Range(1, 10).ToList();

            if (ModelState.IsValid)
            {

                unitRepository.Create(unit);
                var numbers=unit.Lessons.Count();
              

                return RedirectToAction("Index", "Unit", new { CourseId = unit.CourseId });
            }
            return View(unit);
        
        }
      public IActionResult Details(int Id) {
        
            var lessons = unitRepository.GetLessonsofUnit(Id);
            ViewBag.Unit=unitRepository.GetById(Id);
            return PartialView("_UnitDetails",lessons);
        
       }
    }
}
