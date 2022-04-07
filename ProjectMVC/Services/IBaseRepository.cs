using ProjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjectMVC.Services
{
    public interface IBaseRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        T Delete(T entity);
        T Get(int id);
        IEnumerable<T> All();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T FindOne(Expression<Func<T, bool>> predicate);
        void SaveChanges();
    }
}
