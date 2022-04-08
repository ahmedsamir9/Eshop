using LinqKit;
using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectMVC.Services
{
    public interface IProductBaseRepo : IBaseRepository<Product>
    {
        public IEnumerable<Product> GetProductWitPaging(ExpressionStarter<Product> predicate, int pageNumber);
        public int GetTotalProuductPages(ExpressionStarter<Product> predicate);
        public IEnumerable<Product> GetRelatedProducts(Expression<Func<Product, bool>> predicate);
    }
}
