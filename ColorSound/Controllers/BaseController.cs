using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grad_Proj.Controllers
{
    public class BaseController<T> : Microsoft.AspNetCore.Mvc.Controller where T : class
    {
        private readonly IUnitOfWork<T> _entity;

        public BaseController(IUnitOfWork<T> entity)
        {
            _entity = entity;
        }

        public virtual IActionResult Index(string search) {
            var data = _entity.Entity.GetAll().ToList();
            return View(data);
        }

        public virtual IActionResult Create() => View();

        [HttpPost]
        public virtual IActionResult Create(T obj)
        {
            _entity.Entity.Insert(obj);
            _entity.Save();
            return View();
        }
        public virtual IActionResult Edit() => View();

        [HttpPost]
        public virtual IActionResult Edit(T obj)
        {
            _entity.Entity.Update(obj);
            _entity.Save();
            return View();
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            if (id != 0)
            {
                _entity.Entity.Delete(id);
                _entity.Save();
                return new JsonResult(true);
            }

            return new JsonResult(false);
        }

    }
}
