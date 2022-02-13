using Core.Interfaces;
using Grad_Proj.Entites;
using Grad_Proj.Infrastructure;
using Grad_Proj.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Controllers
{
    public class ItemsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ThemesShopDbContext _db;
        private readonly IAuthorService _authorService;
        private readonly IWebHostEnvironment _webhostingEnvironment;

        public ItemsController(ThemesShopDbContext db, IAuthorService authorService, IWebHostEnvironment webhostingEnvironment)
        {
            _db = db;
            _authorService = authorService;
            _webhostingEnvironment = webhostingEnvironment;
        }

        public IActionResult Index(int catId = 0)
        {

            var items = _db
                        .Items
                        .Include(x => x.Category)
                        .Include(x => x.Author)
                        .Include(x=>x.ItemImages)
                        .ToList();

            if (catId != 0)
            {
                items.Where(x => x.CategoryId == catId);
            }

            return View(items ?? new List<Item>());
        }

        public IActionResult ItemDetails(int id)
        {
            var item = _db
                        .Items.Where(x => x.Id == id)
                        .Include(x => x.Category)
                        .Include(x => x.Author)
                        .Include(x => x.ItemImages)
                        .FirstOrDefault();
            return View(item ?? new Item());
        }

        public IActionResult Create(int id=0)
        {
            if(id==0)
            {
                var email = HttpContext.Session.GetString("Email");
                if (email != null)
                {
                    var author = _authorService.GetAuthorByEmail(email);
                    return View(author);
                }
            }else
            {
                var email = _db.Authors.Find(id).Email;
                var author = _authorService.GetAuthorByEmail(email);
                return View(author);
            }
            
            return new RedirectResult("/Account/Login");
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(Item item, List<IFormFile> images)
        {
            var email = HttpContext.Session.GetString("Email");
            item.AuthorId = _authorService.GetAuthorByEmail(email).Id;
            _db.Items.Add(item);
            _db.SaveChanges();

            foreach (var file in images)
            {
                string uploadFolder = Path.Combine(_webhostingEnvironment.WebRootPath, "images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                _db.ItemImages.Add(new ItemImages
                {
                    ItemId = item.Id,
                    Image = uniqueFileName
                });
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Create));

        }

        [HttpPost]
        public void ToggleLikes(int itemId)
        {
            var email = HttpContext.Session.GetString("Email");
            int authorId = _authorService.GetAuthorByEmail(email).Id;
            var item = _db.ItemLikes.Where(x => x.ItemId == itemId).FirstOrDefault();
            if (item == null)
                _db.ItemLikes.Add(new ItemLikes
                {
                    ItemId = itemId,
                    AuthorId = authorId
                });
            else
                _db.Remove(item);

            _db.SaveChanges();
        }

        [HttpPost]
        public void RemoveLike(int id)
        {
            var item = _db.ItemLikes.Find(id); ;
        }
    }
}
