
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Services
{
    public class CatgoryRepoService : ICategoryRepository
    {
        public CatgoryRepoService(ShopDBContext context)
        {
            Context = context;
        }

        public ShopDBContext Context { get; }

        public void Delete(int id)
        {
                 var category =  Context.categories.Find(id);
                 Context.categories.Remove(category);
                 Context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return Context.categories.ToList();
        }

        public Category GetByName(string Name)
        {
            return Context.categories.FirstOrDefault(c => c.Name == Name);  
        }

        public Category GetDetails(int id)
        {
      
            return  Context.categories.FirstOrDefault(m => m.Id == id);
        }

        public void Insert(Category cat)
        {
            Context.Add(cat);
            Context.SaveChanges();
          
        }

        public void Update(int id, Category category)
        {
            Category currCategory=Context.categories.Find(id);
            currCategory.Description = category.Description;
            currCategory.Name = category.Name;
            Context.SaveChanges();
        }
    }
}
