using ProjectMVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace ProjectMVC.Services
{
    public class CartRepoService : ICartRepository
    {
        private readonly ShopDBContext context;
        private readonly IProductBaseRepo productBaseRepo;
        public CartRepoService(ShopDBContext context, IProductBaseRepo productBaseRepo)
        {
            this.context = context;
            this.productBaseRepo = productBaseRepo;
        }
        private bool IsAvailable(int productID, int qty)
        {
            return productBaseRepo.Get(productID).NumInStock >= qty;
        }
        public void AddItem(string clientID, int productID, int qty)
        {
            Product product = context.products.Find(productID);

            if (product == null || !IsAvailable(productID, qty))
                return;

            var isOnCart = context.carts.FirstOrDefault(c => c.ProductId == productID && c.ClientId == clientID) != null;
            if (isOnCart)
                return;

            Cart cart = new Cart();
            cart.ClientId = clientID;
            cart.ProductId = productID;
            cart.dateTime = System.DateTime.Now;
            cart.Quntity = qty;
            // represents total price of this item inside cart
            cart.TotalPrice = product.Price*qty;

            // add product to cart and decrease its numStock by 1
            context.carts.Add(cart);
            product.NumInStock-=qty;
            productBaseRepo.Update(product);

            context.SaveChanges();

        }
        public void ClearCart(string clientID)
        {
            var allItems = context.carts.Where(c => c.ClientId == clientID).ToList();
           
            context.RemoveRange(allItems);
            // return all items to stock 
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

            product.NumInStock++;
            productBaseRepo.Update(product);

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

            //if(!IsAvailable)

            cart.Quntity++;
            cart.TotalPrice += product.Price;

            product.NumInStock--;
            productBaseRepo.Update(product);

            context.SaveChanges(true);
        }

        public void RemoveItem(string clientID, int productID)
        {
            Cart cart = context.carts.FirstOrDefault(c => c.ClientId == clientID && c.ProductId == productID);
            Product product = context.products.Find(productID);
            
            // remove the product from the cart and increase itsNumstock by the removed quantity
            context.carts.Remove(cart);
            product.NumInStock += cart.Quntity;
            productBaseRepo.Update(product);

            context.SaveChanges();
        }
    }
}
