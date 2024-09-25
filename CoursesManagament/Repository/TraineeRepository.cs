using CoursesManagament.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagament.Repository
{
    public class TraineeRepository : ITraineeRepository
    {
        private readonly AppDbContext context;
        public TraineeRepository( AppDbContext context) {
        this.context = context;
        }
        public List<TraineeCourse> GetTraineesByCourse(int Course_id)
        {
           return context.TraineeCourses.Where(t=>t.CourseId == Course_id).Include(t=>t.Trainee).ToList();
        }
        public void create(Trainee trainee)
        {
            context.Trainees.Add(trainee);
            context.SaveChanges();
        }

    }
}
