using CoursesManagament.Attributes;
using CoursesManagament.Models;
using CoursesManagament.Repository;
using CoursesManagament.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace CoursesManagament.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly ITraineeRepository traineeRepository;
        AppDbContext context;



        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager,ITraineeRepository traineeRepository, AppDbContext context)
        {
            _userManager = userManager;
            _signinManager = signinManager;
            this.traineeRepository = traineeRepository;
            this.context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
if(ModelState.IsValid)
            {

                var usermodel =await _userManager.FindByEmailAsync(user.Email);

                if (usermodel!=null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signinManager.PasswordSignInAsync(usermodel, user.Password, user.RememberMe, false);
                    if (result.Succeeded)
                    {
                        List<Claim> claims = new List<Claim> {
                        new Claim("TraineeId", usermodel.TraineeId.ToString()??string.Empty),
                        new Claim("TrainerId", usermodel.TrainerId.ToString() ?? string.Empty)};

                        await _signinManager.SignInWithClaimsAsync(usermodel,user.RememberMe,claims);
                        var roleclaim = User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.Role);
                        var Role = roleclaim?.Value;
                        if(Role!=null)
                        {
                            if (Role == "Admin")
                            {
                              return  RedirectToAction("Index", "Home");
                            }
                            else if (Role == "Trainer")
                            {
                                var Trainer = User.Claims.FirstOrDefault(c => c.Type == "TrainerId");
                                var trainerid = Trainer?.Value;
                               return  RedirectToAction("Index", "Course");
                            }
                            else
                                return RedirectToAction("Default", "Course");
                        }

                        
                        
                    }
                    else
                    {
                        ModelState.AddModelError("", "WrongData!!");
                    }
                }


            }
            return View(user);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register(RegisterViewModel User)
        {
            if (ModelState.IsValid)
            {
                // Create the user
                ApplicationUser usermodel = new ApplicationUser
                {
                    Email = User.Email,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    UserName = User.UserName
                };

                // Create the user in the system
                IdentityResult result = await _userManager.CreateAsync(usermodel, User.Password);

                if (result.Succeeded)
                {
                    // Create a new Trainee record
                    Trainee trainee = new Trainee
                    {
                        CreationDate = DateTime.Now,
                        Email = User.Email,
                        FirstName = User.FirstName,
                        LastName = User.LastName
                    };
                    traineeRepository.create(trainee);

                    // Save the Trainee ID in the user model
                    usermodel.TraineeId = trainee.Id;
                    context.SaveChanges();

                    // Add claims after saving the TraineeId
                    List<Claim> claims = new List<Claim>
            {
                new Claim("TraineeId", trainee.Id.ToString()),
                new Claim("TrainerId", usermodel.TrainerId?.ToString() ?? string.Empty) // You can adjust this based on your logic
            };

                    await _userManager.AddToRoleAsync(usermodel, "Trainee");

                    await _userManager.AddClaimsAsync(usermodel, claims);

                    await _signinManager.SignInWithClaimsAsync(usermodel, false, claims);

                    return RedirectToAction("Default", "Course");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(User);
        }

        public async Task<IActionResult> CheckEmail(string Email,string CurrentEmail)
        {
          
            
                var userEmail = await _userManager.FindByEmailAsync(Email);
                if (userEmail == null || userEmail.Email==CurrentEmail)
                {
                    return Json(true);
                }
                return Json(false);
            
         
        }
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Default","Course");
        }

    }
}
