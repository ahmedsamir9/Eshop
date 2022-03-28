﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using ProjectMVC.Services;

namespace ProjectMVC.Controllers
{
    public class ProductsController : Controller
    {
       
        private readonly IWebHostEnvironment webHostEnvironment;

        public IProductRepository ProductContext { get; }
        public ICategoryRepository CategoryContext { get; }

        public ProductsController(IProductRepository productContext, ICategoryRepository categoryContext, IWebHostEnvironment webhost)
        {
            webHostEnvironment = webhost;
            ProductContext = productContext;
            CategoryContext = categoryContext;
        }
        private string UploadFile(Product product)
        {
            string uniqueFileName = null;

            if (product.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath,"images");
                uniqueFileName=Guid.NewGuid().ToString()+"_"+product.ImageFile.FileName;
                string filePath=Path.Combine(uploadsFolder,uniqueFileName);
                using (var fileStream= new FileStream(filePath,FileMode.Create))
                {
                    product.ImageFile.CopyTo(fileStream);

                }


            }

            return uniqueFileName;
        }

        private void RemoveFile(string imgPath)
        {
            
                string image = Path.Combine(webHostEnvironment.WebRootPath, "images", imgPath);
                FileInfo fi=new FileInfo(image);
            if (fi != null)
            {
                System.IO.File.Delete(image);
                fi.Delete();
            }
           
        }


        // GET: Products
        public ActionResult Index()
        {
            var prdList = ProductContext.GetAll();
            return View(prdList);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            var product =ProductContext.GetDetails(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //// GET: Products/Create
        public IActionResult Create()
        {

            ViewData["CategoryId"] = new SelectList(CategoryContext.GetAll(), "Id", "Name");
            return View();
        }

        //// POST: Products/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadFile(product);
                product.ImgPath = uniqueFileName;
                ProductContext.Insert(product);
              
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(CategoryContext.GetAll(), "Id", "Name");
            return View(product);
        }

        //// GET: Products/Edit/5
        public ActionResult Edit(int id)
        {


            var product = ProductContext.GetDetails(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(CategoryContext.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {

            if (ModelState.IsValid)
            {
                 Product oldProduct = ProductContext.GetDetails(id);
                    if (product.ImageFile == null)
                    {
                        product.ImgPath = oldProduct.ImgPath;
                    }
                    else
                    {
                        RemoveFile(oldProduct.ImgPath);
                        product.ImgPath = UploadFile(product);

                    }
                    ProductContext.Update(id, product);
                    return RedirectToAction(nameof(Index));
             
            }
            ViewData["CategoryId"] = new SelectList(CategoryContext.GetAll(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        //// GET: Products/Delete/5
        public ActionResult Delete(int id)
        {

            var product = ProductContext.GetDetails(id);
              
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product =  ProductContext.GetDetails(id);
            RemoveFile(product.ImgPath);

            ProductContext.Delete(id);
           
            return RedirectToAction(nameof(Index));
        }

       
    }
}
