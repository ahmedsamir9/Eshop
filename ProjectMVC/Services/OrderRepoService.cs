using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ProjectMVC.Services
{
    // Not finished yet
    public class OrderRepoService : GenericRepository<Order>
    {
        public OrderRepoService(ShopDBContext _Context) : base(_Context) { }

        public override Order Add(Order entity)
        {
            Context.orders.Add(entity);
            Context.SaveChanges();
            return Context.orders.FirstOrDefault(o => o.ClientId == entity.ClientId && o.dateTime == entity.dateTime);
        }

        public override IEnumerable<Order> All()
        {
            return Context.orders.ToList();
        }

        public override Order Delete(Order entity)
        {
            Context.orders.Remove(entity);  
            Context.SaveChanges();
            return entity;
        }

        public override IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            return Context.orders.Include(o => o.orderProducts).Where(predicate).OrderByDescending(o => o.dateTime).ToList();
        }

        public override Order Get(int id)
        {
            return Context.orders.Include(o => o.Client).FirstOrDefault(o => o.Id == id);
        }

        //public override Order Update(Order entity)
        //{
        //    return base.Update(entity);
        //}
    }
}
