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

namespace ProjectMVC.Controllers
{

    [Authorize(Roles = "Client")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        string clientID;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            clientID = "52534c23-1187-48b1-9773-df9127f5fd8b";

            //ToDo: get current logged in user id 
        }


        // GET: Cart/Index
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        //GET: Cart/getAllItems
        public ActionResult getAllItems()
        {
            var allItems = _cartRepository.GetAllItems(clientID);
            var orderTotal = allItems.Sum(i => i.TotalPrice);
            ViewBag.orderTotal = orderTotal;
            return PartialView("Partial/Cart", allItems);
        }


        // GET: Cart/Remove/productID

        [Route("Cart/Remove/{productID:int}")]
        public ActionResult Remove([FromRoute] int productID)
        {
            _cartRepository.RemoveItem(clientID, productID);
            return new EmptyResult();   
        }

        // GET: Cart/Clear
        public ActionResult Clear()
        {
            _cartRepository.ClearCart(clientID);
            return new EmptyResult();
        }




        //[Route("Cart/Increase/{productID:int}")]
        // POST: Cart/Increase
        [HttpPost]
        public ActionResult Increase([FromBody] Cart c)
        {
            _cartRepository.IncreaseItemByOne(clientID, c.ProductId);
            return new EmptyResult();
        }

        //[Route("Cart/Decrease/{productID:int}")]
        // POST: Cart/Decrease
        [HttpPost]
        public ActionResult Decrease([FromBody] Cart c)
        {
            _cartRepository.DecreaseItemByOne(clientID, c.ProductId);
            return new EmptyResult();
        }


        // POST: Cart/Add/ProductID
        [HttpPost]
        [Route("Cart/Add/{productID:int}/{qty:int}")]
        public ActionResult Add([FromRoute] int productID, [FromRoute] int qty)
        {
            _cartRepository.AddItem(clientID, productID, qty);
            return new EmptyResult();
        }


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
