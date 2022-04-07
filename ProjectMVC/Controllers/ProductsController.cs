using System;
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
using Microsoft.AspNetCore.Authorization;
using ProjectMVC.ViewModel;
using AutoMapper;
using System.Linq.Expressions;
using LinqKit;
using ProjectMVC.Utils;

namespace ProjectMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
       
        private readonly IImageHandler ImageHandler;

        private readonly IMapper _mapper;
        public IBaseRepository<Category> CategoryRepository { get; }
        public readonly IProductBaseRepo ProductRepository;
        public ProductsController(IMapper mapper, IProductBaseRepo repositoryy, IBaseRepository<Category> categoryContext, IImageHandler imageHandler)
        {
            ImageHandler = imageHandler;
            CategoryRepository = categoryContext;
            _mapper = mapper??throw new ArgumentNullException(nameof(mapper));
            ProductRepository = repositoryy?? throw new ArgumentNullException(nameof(repositoryy));
        }
      
        // GET: Products
        [AllowAnonymous]
        public ActionResult Index()
        {
            var prdList = ProductRepository.All();
            return View(prdList);
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var product = ProductRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //// GET: Products/Create
        public IActionResult Create()
        {

            ViewData["CategoryId"] = new SelectList(CategoryRepository.All(), "Id", "Name");
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
                string uniqueFileName = ImageHandler.UploadImage(product);
                product.ImgPath = uniqueFileName;
                ProductRepository.Add(product);
                ProductRepository.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(CategoryRepository.All(), "Id", "Name");
            return View(product);
        }

        //// GET: Products/Edit/5
        public ActionResult Edit(int id)
        {


            var product = ProductRepository.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(CategoryRepository.All(), "Id", "Name", product.CategoryId);
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

              if (product.ImageFile!=null)
                {
                    ImageHandler.RemoveImage(product.ImgPath);
                    product.ImgPath = ImageHandler.UploadImage(product);

                }

                    ProductRepository.Update( product);
                    ProductRepository.SaveChanges();
                    return RedirectToAction(nameof(Index));
             
            }
            ViewData["CategoryId"] = new SelectList(CategoryRepository.All(), "Id", "Name", product.CategoryId);
            return View(product);
        }

        //// GET: Products/Delete/5
        public ActionResult Delete(int id)
        {

            var product = ProductRepository.Get(id);
              
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
            var product = ProductRepository.Get(id);
            ImageHandler.RemoveImage(product.ImgPath);

            ProductRepository.Delete(product);
            ProductRepository.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult ProductDetails(int id)
        {
            var product = ProductRepository.Get(id);

            Expression<Func<Product, bool>> predicate = 
                e => e.CategoryId == product.CategoryId && e.Id != product.Id;
            
            var relatedProducts =
                ProductRepository.GetRelatedProducts(predicate)
                .Select(e=> _mapper.Map<ProductVM>(e));
            
            ViewBag.RelatedProducts = relatedProducts; 
            
            return View("ProductDetails",_mapper.Map<ProductVM>(product) );
        }
        [AllowAnonymous]
        public IActionResult Shop()
        {
            int pageNumber = 1;
            var predicate = PredicateBuilder.True<Product>();
            var products =
                ProductRepository.GetProductWitPaging(predicate,pageNumber).
                Select(p=>_mapper.Map<ProductVM>(p));
            ViewBag.Categories = CategoryRepository.All();
            ViewBag.PagesCount = ProductRepository.GetTotalProuductPages(predicate);
            return View("ShopNavigator",products);
        }

    }
}
