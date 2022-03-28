using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using ProjectMVC.Services;

namespace ProjectMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _context;

        public CategoriesController(ICategoryRepository context)
        {
            _context = context;
        }

        // GET: Categories
        public ActionResult  Index()
        {
            return View(_context.GetAll());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int id)
        {
            var category = _context.GetDetails(id);
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
                _context.Insert(category);
              
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //    // GET: Categories/Edit/5
        public ActionResult Edit(int id)
        {

            var category = _context.GetDetails(id);
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
               
                _context.Update(id,category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //    // GET: Categories/Delete/5
        public ActionResult Delete(int id)
        {
           
            var category = _context.GetDetails(id);
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
           
            _context.Delete(id);
           
            return RedirectToAction(nameof(Index));
        }

    }

}
