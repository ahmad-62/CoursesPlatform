using CoursesManagament.Models;

namespace CoursesManagament.Repository
{
    public interface ICourseRepository
    {
        
        public void Create(Course course);
        public List<Course> GetAll(int? categoryid, string? query, int? trainerid = null);
        public void Update(Course course,int id);
        public void Delete(int id);
        public Course GetById(int id);
        public bool AddTraineeToCourse(int courseId, int traineeId);
    }
}
