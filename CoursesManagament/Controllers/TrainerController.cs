using CoursesManagament.Models;
using CoursesManagament.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace CoursesManagament.Controllers
{
    [Authorize(Roles = "Admin")]

    public class TrainerController : Controller
    {
            ITrainerRepository trainer;
        private readonly UserManager<ApplicationUser> userManager;
        AppDbContext context;
        private readonly SignInManager<ApplicationUser> signInManager;
            public TrainerController(ITrainerRepository trainerRepository,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser>signInManager,AppDbContext context) {
        
            trainer = trainerRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            }
            public IActionResult Create()
            {
                return View();
            }
            [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task <IActionResult> Create(Trainer _trainer)
            {
            if (ModelState.IsValid == true)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = _trainer.FirstName;
                user.LastName = _trainer.LastName;
                user.Email = _trainer.Email;
                user.UserName = _trainer.username;
                user.PasswordHash = _trainer.Password;

                IdentityResult result = await userManager.CreateAsync(user, _trainer.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Trainer");

                        trainer.Create(_trainer);
                    user.TrainerId = _trainer.Id;
                    context.SaveChanges();
                    


                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                }
            }
                return View(_trainer);
            }
        public IActionResult CheckEmail(string Email) { 
var _trainer=trainer.GetByEmail(Email);
            if (_trainer==null)

            {
                return Json(true);
            }
            return Json(false);



        }
        public IActionResult Index( ) { 
        
        return View(trainer.GetAll());
        }
        public IActionResult edit(int id )
        {
            var _trainer=trainer.GetById(id);
            return View(_trainer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> edit(int id,Trainer _trainer) {

            if (ModelState.IsValid == true)
            {
               if( await trainer.UpdateAsync(_trainer, id,ModelState))
                    return RedirectToAction("Index");
               else
                    return View(_trainer);

            }
            return View(_trainer);

        
        }
        public IActionResult Delete(int id)
        {
           trainer.Delete(id);
            return RedirectToAction("Index");

        }
        
    }
}
