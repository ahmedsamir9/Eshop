using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjectMVC.Services
{

    public abstract class GenericRepository<T>
        : IBaseRepository<T> where T : class
    {
        public GenericRepository(ShopDBContext _Context)
        {
            Context = _Context;
        }

        public ShopDBContext Context { get; }

        public virtual T Add(T entity)
        {
            return Context
                .Add(entity)
                .Entity;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
          
            return Context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToList();
        }

        public virtual T Get(int id)
        {
            return Context.Find<T>(id);
        }

        public virtual IEnumerable<T> All()
        {
            return Context.Set<T>()
                .ToList();
        }

        public virtual T Update(T entity)
        {
            return Context.Update(entity)
                .Entity;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual T Delete(T entity)
        {
            return Context.Remove(entity)
               .Entity;
        }

    }
}
