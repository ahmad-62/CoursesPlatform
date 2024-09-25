using CoursesManagament.Models;

namespace CoursesManagament.Repository
{
    public interface ICategoryRepoistory
    {
        public List<Category> GetAll();
        public void Create(Category category);
        public Category GetById(int id);
        public void Update(int id,Category category);
        public bool Delete(int id); 
    }
}
