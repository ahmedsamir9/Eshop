using ProjectMVC.Models;
using System.Collections.Generic;

namespace ProjectMVC.Services
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product GetDetails(int id);
        public void Insert(Product product);
        public void Update(int id, Product product);
        public void Delete(int id);
    }
}
