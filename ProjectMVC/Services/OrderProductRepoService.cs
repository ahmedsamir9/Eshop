using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectMVC.Services
{
    public class OrderProductRepoService : GenericRepository<OrderProduct>
    {
        public OrderProductRepoService(ShopDBContext _Context) : base(_Context) { }

        public override OrderProduct Add(OrderProduct entity)
        {
            Context.OrderProducts.Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public override IEnumerable<OrderProduct> Find(Expression<Func<OrderProduct, bool>> predicate)
        {
            return Context.OrderProducts.Include(op => op.Product).Where(predicate).ToList();
        }
    }
}
