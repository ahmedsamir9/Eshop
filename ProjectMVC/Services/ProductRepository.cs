using ProjectMVC.Models;
using System.Collections.Generic;

namespace ProjectMVC.Services
{
    public class ProductRepositoryy : GenericRepository<Product>
    {
        public ProductRepositoryy(ShopDBContext context) : base(context) { }
       
    }
}
