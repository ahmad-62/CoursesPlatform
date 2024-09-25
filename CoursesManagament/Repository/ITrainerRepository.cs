using CoursesManagament.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoursesManagament.Repository
{
    public interface ITrainerRepository
    {
        public List<Trainer> GetAll();
        public Trainer GetById(int id);
        public void Create(Trainer trainer);
        public Trainer GetByEmail(string Email);
        public  Task<bool> UpdateAsync(Trainer trainer,int id,ModelStateDictionary modelstate);
        public void Delete(int id);



    }
}
