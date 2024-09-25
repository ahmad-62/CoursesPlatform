using CoursesManagament.Models;

namespace CoursesManagament.Repository
{
    public interface ITraineeRepository
    {
        public List<TraineeCourse> GetTraineesByCourse(int Course_id);
        public void create(Trainee trainee);
        
    }
}
