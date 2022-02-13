using Grad_Proj.Entites;
using Grad_Proj.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Controllers
{
    public class ContactUsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ThemesShopDbContext _db;

        public ContactUsController(ThemesShopDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(ContactUs obj)
        {
            _db.ContactUs.Add(obj);
            _db.SaveChanges();
            return View();
        }

    }
}
