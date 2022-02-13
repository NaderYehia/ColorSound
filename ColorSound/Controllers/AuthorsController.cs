using Core.Interfaces;
using Grad_Proj.Entites;
using Grad_Proj.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Controllers
{
    public class AuthorsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ThemesShopDbContext _db;
        public AuthorsController(ThemesShopDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var authors = _db
                            .Authors
                            .Include(x => x.Followers)
                            .Include(x => x.Following)
                            .ToList();
            return View(authors ?? new List<Author>());
        }
        public IActionResult AuthorDetials(int id)
        {
            var author = _db.Authors.Find(id);
            return View(author ?? new Author());
        }

        [HttpPost]
        public dynamic ToggleFollow(int id)
        {
            int? currentUserId = HttpContext.Session.GetInt32("Id");
            if (currentUserId != null || currentUserId != 0)
            {
                var follow = _db.AuthorToAuthor.Where(x => x.AuthorId == id && x.FollowerId == currentUserId).FirstOrDefault();
                if (follow == null)
                {
                    _db.AuthorToAuthor.Add(new AuthorToAuthor
                    {
                        AuthorId = id,
                        FollowerId = currentUserId
                    });
                }
                else
                {
                    _db.AuthorToAuthor.Remove(follow);
                }
                return _db.SaveChanges() > 0;
            }
            else
                return RedirectToAction("Login", "Account");
        }
    }
}
