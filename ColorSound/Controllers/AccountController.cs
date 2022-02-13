using Grad_Proj.Entites;
using Grad_Proj.Infrastructure;
using Grad_Proj.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Controllers
{
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IWebHostEnvironment _webhostingEnvironment;
        private readonly ThemesShopDbContext _db;
        public AccountController(IAuthorService authorService, IWebHostEnvironment webhostingEnvironment, ThemesShopDbContext db)
        {
            _authorService = authorService;
            _webhostingEnvironment = webhostingEnvironment;
            _db = db;
        }

        public IActionResult Login()
        {
            return View(new Author());
        }
        [HttpPost]
        public IActionResult Login(Author author)
        {
            var result = _authorService.IsValidAuthor(author);
            if (result)
            {
                author = _authorService.GetAuthorByEmail(author.Email);
                HttpContext.Session.SetString("NickName", author.FirstName);
                HttpContext.Session.SetString("Email", author.Email);
                HttpContext.Session.SetInt32("Id", author.Id);
                return new RedirectResult("/Items/Index");
            }
            return View();
        }
        public IActionResult Register()
        {
            ViewBag.Exist = "";
            return View(new Author());
        }
        [HttpPost]
        public IActionResult Register(Author author)
        {
            if (!_authorService.IsExist(author.Email))
            {
                if (_authorService.RegisterAuthor(author))
                    return RedirectToAction(nameof(Login));

            }
            else
            {
                ViewBag.Exist = "This Email Already Exist";
            }

            return View(author);
        }
        [HttpPost]
        public bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("Email") != null;
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("Email", "");
            return RedirectToAction(nameof(Login));
        }
        public IActionResult Edit()
        {
            var email = HttpContext.Session.GetString("Email");
            var author = _authorService.GetAuthorByEmail(email);
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author, IFormFile image, IFormFile cover)
        {
            var email = HttpContext.Session.GetString("Email");
            var oldData = _authorService.GetAuthorByEmail(email);
            if (oldData != null)
            {
                oldData.Password = author.Password;
                oldData.PortfolioLink = author.PortfolioLink;
                if (image != null)
                {
                    string uploadFolder = Path.Combine(_webhostingEnvironment.WebRootPath, "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    oldData.Image = uniqueFileName;
                }
                if (cover != null)
                {
                    string uploadFolder = Path.Combine(_webhostingEnvironment.WebRootPath, "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + cover.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        cover.CopyTo(fileStream);
                    }
                    oldData.Cover = uniqueFileName;
                }

                oldData.PortfolioLink = author.PortfolioLink;
            }
            _db.Authors.Update(oldData);
            _db.SaveChanges();
            return Redirect("/Items/Create");
        }
    }
}
