using CoursesManagament.Models;

namespace CoursesManagament.Repository
{
    public interface IUnitRepository
    {
        public List<Units> GetCourseUnits(int CourseId);
        public void Create(Units unit);
        public List<Lesson> GetLessonsofUnit(int unitId);
        public Units GetById(int unitId);
    }
}
