using ProjectMVC.Models;
using System.Collections.Generic;

namespace ProjectMVC.Services
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(ShopDBContext context) : base(context) { }
       
    }
}
