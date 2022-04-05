﻿using LinqKit;
using ProjectMVC.Models;
using ProjectMVC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectMVC.Services
{

    public class ProductRepositoryy : GenericRepository<Product> ,IProductBaseRepo 
    {
        public ProductRepositoryy(ShopDBContext context) : base(context) { }


        public IEnumerable<Product> GetProductWitPaging(ExpressionStarter<Product> predicate, int pageNumber) 
        {
            return Context.products.Where(predicate)
            .Skip((pageNumber - 1)* Constants.PRODUCT_NUMBERS).Take(Constants.PRODUCT_NUMBERS);      
        }
        public IEnumerable<Product> GetRelatedProducts(Expression<Func<Product,bool>> predicate)
        {
            return Context.products.AsQueryable()
                .Where(predicate).Take(Constants.RELATED_PRODUCT_NUMBER);
          
        }

        public int GetTotalProuductPages(ExpressionStarter<Product> predicate)
        {
            return (int)Math.Ceiling((decimal)Context.products.Where(predicate).Count() / Constants.PRODUCT_NUMBERS);

        }
    }
}
