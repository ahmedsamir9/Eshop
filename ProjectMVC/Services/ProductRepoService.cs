using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectMVC.Services
{
    public class ProductRepoService : IProductRepository
    {
        public ProductRepoService(ShopDBContext context) {
            Context = context;
        }

        public ShopDBContext Context { get; }

        public void Delete(int id)
        {
            Context.products.Remove(Context.products.Find(id));
            Context.SaveChanges();
        }

        public List<Product> GetAll()
        {
           return Context.products.AsSplitQuery().Include(p => p.Category).ToList();
            //left join cartisan 10*10 = 100 -> left 
            // select 
            // inner join category 
        }

        public Product GetDetails(int id)
        {
            return Context.products.Include(p => p.Category).FirstOrDefault(p=>p.Id==id);
        }

        public void Insert(Product product)
        {
            Context.products.Add(product);
            Context.SaveChanges();
        }

        public void Update(int id, Product product)
        {
            Product currProduct = Context.products.Find(id);
            currProduct.Name = product.Name;
            currProduct.Description=product.Description;
            currProduct.Price= product.Price;
            currProduct.NumInStock = product.NumInStock;
            currProduct.ImgPath = product.ImgPath;
            currProduct.CategoryId= product.CategoryId; 
            Context.SaveChanges();

        }

        
    }
}
