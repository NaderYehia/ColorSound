using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Grad_Proj.Infrastructure;

namespace Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ThemesShopDbContext _context;
        private DbSet<T> table = null;
        public GenericRepository(ThemesShopDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            try
            {
                var entity = GetById(id);
                if (entity != null)
                {
                    Update(entity);
                }
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = table;
            return query;
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public T GetFirst()
        {
            return table.FirstOrDefault();
        }
        public ICollection<T> Get(Expression<Func<T, bool>> where, Expression<Func<T, T>> select) 
        {
            return table.Where(where).Select(select).ToList();
        }
        public void Insert(T entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Add(entity);
                }
            }
            catch (Exception dbEx)
            {
                throw dbEx;
            }
        }

        public void Update(T entity)
        {
            var contextEntity = _context.Entry<T>(entity);
            if (contextEntity != null)
            {
                if (contextEntity.State == EntityState.Detached)
                {
                    _context.Attach(entity);
                }
                contextEntity.State = EntityState.Modified;
            }
        }
    }
}
