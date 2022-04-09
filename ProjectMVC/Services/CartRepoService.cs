using ProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace ProjectMVC.Services
{
    public class CartRepoService : ICartRepository
    {
        private readonly ShopDBContext context;

        public CartRepoService(ShopDBContext context)
        {
            this.context = context;
        }

        public void AddItem(string clientID, int productID,int qauntity)
        {
            Product product = context.products.Find(productID);

            var isOnCart = context.carts.FirstOrDefault(c => c.ProductId == productID && c.ClientId == clientID) != null;
            if (isOnCart)
                return;

            Cart cart = new Cart();
            cart.ClientId = clientID;
            cart.ProductId = productID;
            cart.dateTime = System.DateTime.Now;
            cart.Quntity = qauntity;

            // represents total price of this item inside cart
            cart.TotalPrice = product.Price*qauntity;

            context.carts.Add(cart);
            context.SaveChanges();

        }

        public void ClearCart(string clientID)
        {
            var allItems = context.carts.Where(c => c.ClientId == clientID).ToList();
            context.RemoveRange(allItems);
            context.SaveChanges();
        }

        public void DecreaseItemByOne(string clientID, int productID)
        {
            Product product = context.products.Find(productID);
            Cart cart = context.carts.FirstOrDefault(c => c.ClientId == clientID && c.ProductId == productID);
            cart.Quntity--;
            cart.TotalPrice -= product.Price;

            if(cart.Quntity == 0)
                context.carts.Remove(cart);
            context.SaveChanges(true);
        }

        public List<Cart> GetAllItems(string clientID)
        {
            return context.carts.Where(c => c.ClientId == clientID).Include(c => c.Product).ToList();
        }

        public void IncreaseItemByOne(string clientID, int productID)
        {
            Product product = context.products.Find(productID);
            Cart cart = context.carts.FirstOrDefault(c => c.ClientId == clientID && c.ProductId == productID);
            cart.Quntity++;
            cart.TotalPrice += product.Price;

            context.SaveChanges(true);
        }

        public void RemoveItem(string clientID, int productID)
        {
            Cart cart = context.carts.FirstOrDefault(c => c.ClientId == clientID && c.ProductId == productID);
            context.carts.Remove(cart);
            context.SaveChanges();
        }
    }
}
