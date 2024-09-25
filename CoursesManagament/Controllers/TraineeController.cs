using CoursesManagament.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursesManagament.Controllers
{
    [Authorize(Roles ="Admin,Trainer")]
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository traineeRepository;
        public TraineeController(ITraineeRepository traineeRepository){
            this.traineeRepository = traineeRepository;

}
        public IActionResult Index(int id)
        {
            var trainees =traineeRepository.GetTraineesByCourse(id);
            
            return View(trainees);
        }
    }
}
