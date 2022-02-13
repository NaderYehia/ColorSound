using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        T GetFirst();
        ICollection<T> Get(Expression<Func<T, bool>> where, Expression<Func<T, T>> select);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
