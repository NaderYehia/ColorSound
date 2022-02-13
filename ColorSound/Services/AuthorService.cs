using Core.Interfaces;
using Grad_Proj.Entites;
using Grad_Proj.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Grad_Proj.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ThemesShopDbContext _db;
        public AuthorService(ThemesShopDbContext db)
        {
            _db = db;
        }

        public bool IsExist(string email)
        {
            return _db.Authors.Any(x => x.Email == email);
        }

        public bool IsValidAuthor(Author user)
        {
            return _db.Authors.Any(x => x.Email == user.Email && x.Password == user.Password);
        }

        public bool RegisterAuthor(Author user)
        {
            _db.Authors.Add(user);
            return _db.SaveChanges() > 0;
        }
        public Author GetAuthorByEmail(string email)
        {
            return _db
                .Authors
                .Where(x => x.Email == email)
                .Include(x => x.Items)
                .ThenInclude(x => x.ItemImages)
                .Include(x => x.Items)
                .ThenInclude(x => x.ItemLikes)
                .Include(x => x.Followers)
                .Include(x => x.Following)
                .FirstOrDefault();
        }
    }
}
