using LinqKit;
using ProjectMVC.Models;
using ProjectMVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectMVC.Services
{
    public class ProductRepositoryy : GenericRepository<Product>
    {
        public ProductRepositoryy(ShopDBContext context) : base(context) { }


        public IEnumerable<Product> GetProductWitPaging(ExpressionStarter<Product> predicate, int pageNumber) 
        {
            return Context.products.Where(predicate)
            .Skip(pageNumber - 1).Take(Constants.PRODUCT_NUMBERS);      
        }
        public int GetTotalProuductPages(ExpressionStarter<Product> predicate) {
            return (int)Math.Ceiling((decimal)Context.products.Where(predicate).Count()/Constants.PRODUCT_NUMBERS);
        }
       
    }
}
