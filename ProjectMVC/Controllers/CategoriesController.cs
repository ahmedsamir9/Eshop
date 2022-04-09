using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using ProjectMVC.Services;

namespace ProjectMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly IBaseRepository<Category> _context;

        public CategoriesController(IBaseRepository<Category> context)
        {
            _context = context;
        }

        // GET: Categories
        [AllowAnonymous]
        public ActionResult  Index()
        {
            return View(_context.All());
        }

        // GET: Categories/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var category = _context.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //    // GET: Categories/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                _context.SaveChanges();
              
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //    // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {

            var category = _context.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //    // POST: Categories/Edit/5
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                _context.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //    // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
           
            var category = _context.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //    // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _context.Get(id);
            _context.Delete(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Exsist(string Name, int id)
        {
            Expression<Func<Category, bool>> predicate = c => c.Name == Name;
            var category = _context.FindOne(predicate);
         
                if (category == null)
                    return Json(true);
                else
                {
                    if (category.Id == id)
                        return Json(true);
                    return Json(false);
                }
           
        }

    }

}
