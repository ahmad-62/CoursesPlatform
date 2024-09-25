using CoursesManagament.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagament.Repository
{
    public class UnitRepository : IUnitRepository
    {
        private readonly AppDbContext context;
        public UnitRepository(AppDbContext context) {
        this.context = context;
        }
        public List<Units> GetCourseUnits(int CourseId)
        {
            return context.Units.Where(x=>x.CourseId == CourseId).Include(x=>x.Lessons).ToList();

        }
        public void Create(Units unit)
        {
            context.Units.Add(unit);
            context.SaveChanges();
        }

        public List<Lesson> GetLessonsofUnit(int unitId)
        {
           return  context.Lessons.Where(x=>x.UnitId == unitId).ToList();
        }

        public Units GetById(int unitId)
        {
            return context.Units.FirstOrDefault(x=>x.Id==unitId);

        }
    }
}
