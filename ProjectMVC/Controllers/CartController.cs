using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using ProjectMVC.Services;
using System;
using ProjectMVC.Models;

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
            clientID = "75edb000-05a0-4889-bd96-2cef1e052532";
            //clientID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }


        // GET: Cart/Index
        public ActionResult Index()
        {
            return View(_cartRepository.GetAllItems(clientID));
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
        [Route("Cart/Add/{productID:int}")]
        public ActionResult Add([FromRoute] int productID)
        {
            try
            {
                _cartRepository.AddItem(clientID, productID);
                return Content("Added!");

            }
            catch (Exception e)
            {
                return Content($"Error : {e.Message}");
            }

            //return new EmptyResult();
        }

    }
}
