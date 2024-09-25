using CoursesManagament.Models;

namespace CoursesManagament.Repository
{
    public class CategoryRepository:ICategoryRepoistory
    {
        AppDbContext Context; 
        public CategoryRepository() {

            Context = new AppDbContext();
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }
        public void Create (Category category) { 
        
            Context.Categories.Add(category);
            Context.SaveChanges();
        }
        public Category GetById(int id)
        {
            return Context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public void Update(int id,Category category)
        {
            var oldcat=GetById(id);
            oldcat.Name = category.Name;
            oldcat.ParentId = category.ParentId;
            Context.SaveChanges();            
        }
        public bool Delete(int id)
        {  
            var category=GetById(id);
            var _category= Context.Categories.FirstOrDefault(x => x.ParentId == id);
            if (_category==null)
            {
                Context.Categories.Remove(category);
                Context.SaveChanges();
                return true;
            }
         return false;

            
        }
    }
}
