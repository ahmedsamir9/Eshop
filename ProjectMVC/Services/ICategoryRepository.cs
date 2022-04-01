using ProjectMVC.Models;
using System.Collections.Generic;

namespace ProjectMVC.Services
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll();
        public Category GetDetails(int id);
        public Category GetByName(string Name);
        public void Insert(Category cat);
        public void Update(int id, Category category);
        public void Delete(int id);

    }
}
