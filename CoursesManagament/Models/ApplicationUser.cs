using Microsoft.AspNetCore.Identity;

namespace CoursesManagament.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? TraineeId { get; set; }
        public virtual Trainee Trainee { get; set; }

        public int? TrainerId { get; set; }
        public virtual Trainer Trainer { get; set; }
    }
}
