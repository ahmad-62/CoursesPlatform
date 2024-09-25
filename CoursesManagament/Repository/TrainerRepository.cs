using CoursesManagament.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CoursesManagament.Repository
{
    public class TrainerRepository : ITrainerRepository
    {
        AppDbContext Context;
        UserManager<ApplicationUser> usermanager;
        public TrainerRepository(AppDbContext context,UserManager<ApplicationUser> usermanager) {
        
            this.Context = context;
            this.usermanager = usermanager;
        }

        public List<Trainer> GetAll()
        {
            return Context.Trainers.ToList();
        }
        
        public void Create(Trainer trainer)
        {
            
            Context.Trainers.Add(trainer);
            trainer.CreationDate = DateTime.Now;
            Context.SaveChanges();
        }

        public Trainer GetById(int id)
        {
            return Context.Trainers.FirstOrDefault(t => t.Id == id);
        }
        public Trainer GetByEmail(String Email) {
            return Context.Trainers.FirstOrDefault(x => x.Email == Email);
        }
        public async Task<bool> UpdateAsync(Trainer trainer,int id,ModelStateDictionary modelstate)
        {
            var OldTrainer= Context.Trainers.FirstOrDefault(x=>x.Id==id);
           var user=await usermanager.FindByNameAsync(OldTrainer.username);
            user.FirstName = trainer.FirstName;
            user.LastName = trainer.LastName;
            user.Email = trainer.Email;
            user.UserName=trainer.username;
            OldTrainer.FirstName= trainer.FirstName;
           OldTrainer.LastName= trainer.LastName;
            OldTrainer.Email= trainer.Email;
            OldTrainer.WebSite = trainer.WebSite;
            OldTrainer.Descripition= trainer.Descripition;
            OldTrainer.username= trainer.username;

            var result=await usermanager.UpdateAsync(user);
            if (result.Succeeded)
            {
                Context.SaveChanges();
                return true;
            }
            else
            {
                foreach (var error in result.Errors) {
                    modelstate.AddModelError("",error.Description);
                }
                return false;
            }
              
                
            
        }
        public void Delete(int id)
        {
            var _trainer=GetById(id);
            Context.Trainers.Remove(_trainer);
            Context.SaveChanges();
        }

      
    }
}
