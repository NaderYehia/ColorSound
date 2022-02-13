using Core.Interfaces;
using Grad_Proj.Infrastructure;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly ThemesShopDbContext _context;
        private IGenericRepository<T> _entity;
        public UnitOfWork(ThemesShopDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }

        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
