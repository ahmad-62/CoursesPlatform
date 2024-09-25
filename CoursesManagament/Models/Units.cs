using CoursesManagament.Attributes;

namespace CoursesManagament.Models
{
    public class Units
    {
        public int Id { get; set; }
        [UniqueUnit]
        public string Name { get; set; }
        public int CourseId {  get; set; }
        public Course? Course { get; set; }
        public List<Lesson> Lessons { get; set; }=new List<Lesson>();
    }
}
