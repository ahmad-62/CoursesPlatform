using CoursesManagament.Models;
using CoursesManagament.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursesManagament.Controllers
{
    [Authorize(Roles = "Admin")]

    public class CategoryController : Controller
    {

        ICategoryRepoistory categoryRepoistory;
        AppDbContext Context;
        public CategoryController(ICategoryRepoistory _categoryRepoistory,AppDbContext dbContext)
        {

            categoryRepoistory = _categoryRepoistory;
            Context = dbContext;
        }
      
        public IActionResult Index()
        {
            var Categories = categoryRepoistory.GetAll();
            return View(Categories);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = categoryRepoistory.GetAll();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid == true)
            {
                categoryRepoistory.Create(category);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = categoryRepoistory.GetAll();


            return View(category);
        }
        public IActionResult Edit(int id)
        {
            var categories = categoryRepoistory.GetAll();
            var category = categoryRepoistory.GetById(id);
            if (category!=null)
            {
               var parentCategory = categories.FirstOrDefault(c => c.Id == id);
                if (parentCategory != null)
                {
                    categories.Remove(parentCategory);
                }
            }
            ViewBag.Categories = categories;

            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category,int id)
        {
            if (ModelState.IsValid == true)
            {
                categoryRepoistory.Update(id,category);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = categoryRepoistory.GetAll();

            return View(category);
        }
        public IActionResult Delete(int id) {
           var result= categoryRepoistory.Delete(id);
            if (!result)
                TempData["ErrorMessage"] = "You cannot Delete This Category Because it using as a Parent ";
            else
                TempData["SuccessMessage"] = "Delete Succesfully";
            return RedirectToAction("Index");
        
        }
        public IActionResult CheckCategory(string Name,string CurrentName) {
            
            var cat=Context.Categories.FirstOrDefault(c => c.Name == Name);

            if (cat == null||cat.Name==CurrentName)
            {
                return Json(true);
            }
            return Json(false);
        
        
        }

      
    }
}

