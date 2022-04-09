using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ProjectMVC.Services;
using System;
using ProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMVC.Controllers
{

    [Authorize(Roles = "Client")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        public UserManager<IdentityUser> UserManager { get; }
        private static string clientID = "";
        public CartController(ICartRepository cartRepository ,UserManager<IdentityUser> _UserManager )
        {
            _cartRepository = cartRepository;
            UserManager = _UserManager;
           
             
        }


        // GET: Cart/Index
        public async Task<IActionResult> Index()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            clientID = user.Id;
            return View();
        }


        [HttpGet]
        //GET: Cart/getAllItems
        public async Task<IActionResult> getAllItems()
        {
            var allItems = _cartRepository.GetAllItems(clientID);
            var orderTotal = allItems.Sum(i => i.TotalPrice);
            ViewBag.orderTotal = orderTotal;
            return PartialView("Partial/Cart", allItems);
        }


        // GET: Cart/Remove/productID

        [Route("Cart/Remove/{productID:int}")]
        public async Task<IActionResult> Remove([FromRoute] int productID)
        {
            _cartRepository.RemoveItem(clientID, productID);
            return new EmptyResult();   
        }

        // GET: Cart/Clear
        public async Task<IActionResult> Clear()
        {
            _cartRepository.ClearCart(clientID);
            return new EmptyResult();
        }




        //[Route("Cart/Increase/{productID:int}")]
        // POST: Cart/Increase
        [HttpPost]
        public async Task<IActionResult> Increase([FromBody] Cart c)
        {
            _cartRepository.IncreaseItemByOne(clientID, c.ProductId);
            return new EmptyResult();
        }

        //[Route("Cart/Decrease/{productID:int}")]
        // POST: Cart/Decrease
        [HttpPost]
        public async Task<IActionResult> Decrease([FromBody] Cart c)
        {
            _cartRepository.DecreaseItemByOne(clientID, c.ProductId);
            return new EmptyResult();
        }


        // POST: Cart/Add/ProductID
        //[HttpPost]
        //[Route("Cart/Add/{productID:int}")]
        //public ActionResult Add([FromRoute] int productID)
        //{
        //    _cartRepository.AddItem(clientID, productID);
        //    return new EmptyResult();
        //}


        [HttpGet]
        public ActionResult PaymentSuccessful()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PaymentFailed()
        {
            return View();
        }

    }
}
