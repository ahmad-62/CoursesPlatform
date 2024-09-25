using CoursesManagament.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagament.Repository
{
    public class CourseRepository : ICourseRepository
    {
        AppDbContext Context;
        ITrainerRepository trainerRepository;
        ICategoryRepoistory categoryRepository;
        private readonly IWebHostEnvironment _environment;


        public CourseRepository(AppDbContext context,ICategoryRepoistory categoryRepoistory,ITrainerRepository trainerRepository,IWebHostEnvironment environment) {

        this.trainerRepository = trainerRepository;
            this.categoryRepository=categoryRepoistory;
        this.Context = context;
           
            _environment = environment;
        }

        public void Create(Course course)
        {
            if (course.ImageFile != null)
            {

                var filename = Path.GetFileName(course.ImageFile.FileName);
                var FullPath = Path.Combine(_environment.WebRootPath, "Uploads", filename);

                using (var stream = new FileStream(FullPath, FileMode.Create))
                {
                    course.ImageFile.CopyTo(stream);
                }
                course.ImageId = "/Uploads/" + filename;
            }
            course.CreationDate = DateTime.Now; 
            Context.Courses.Add(course);
            Context.SaveChanges();  
        }

      
        public List<Course> GetAll(int? categoryid=null,string? query=null,int? trainerid = null)
        {
            var courses = Context.Courses.Where(x=>( trainerid == null || x.TrainerId == trainerid)&&( categoryid == null||x.CategoryId==categoryid)&&(query ==null||x.Name.Contains(query)))
                        .Include(c => c.Category)
                        .Include(c => c.Trainer)
                        .ToList();
            
            return courses;
;
        }
        public Course GetById(int id)
        {
            return Context.Courses
                            .Include(c => c.Trainer)
                            .FirstOrDefault(c => c.Id == id);
        }

        public void Update(Course course, int id)
        {
            var oldcourse = GetById(id);
            oldcourse.Name = course.Name;
            oldcourse.Descripition=course.Descripition;
            oldcourse.TrainerId = course.TrainerId;
            oldcourse.CategoryId = course.CategoryId; 
            oldcourse.Price = course.Price;
           //oldcourse.ImageId= course.ImageId;
          if (course.ImageFile != null)
            {

                var filename = Path.GetFileName(course.ImageFile.FileName);
                var FullPath = Path.Combine(_environment.WebRootPath, "Uploads", filename);

                using (var stream = new FileStream(FullPath, FileMode.Create))
                {
                    course.ImageFile.CopyTo(stream);
                }
                oldcourse.ImageId = "/Uploads/" + filename;

              //  string oldfilename=GetById(oldcourse.Id).ImageId;
                //string fulloldpath = Path.Combine(_environment.WebRootPath, oldfilename);
               
             //   course.ImageId = "/Uploads/" + filename;
                oldcourse.ImageId="/Uploads/"+filename;


            }
            Context.SaveChanges();




        }
      
        public void Delete(int id)
        {
            var Delcourse=GetById(id);
            Context.Courses.Remove(Delcourse);
            Context.SaveChanges();
        }

        public bool AddTraineeToCourse(int courseId, int traineeId)
        {
            var existingTraineeCourse = Context.TraineeCourses.FirstOrDefault(x => x.CourseId == courseId && x.TraineeId == traineeId);

            if (existingTraineeCourse == null)
            {
                TraineeCourse traineeCourse = new TraineeCourse
                {
                    TraineeId = traineeId,
                    CourseId = courseId,
                    RegistrationDate = DateTime.Now
                };

                Context.TraineeCourses.Add(traineeCourse);
                Context.SaveChanges();
                return true;
            }

            return false;
        }




    }
}
