using ProjectMVC.Models;
using System.Collections.Generic;

namespace ProjectMVC.Services
{
    public interface ICartRepository
    {
        public List<Cart> GetAllItems(string clientID);
        public void AddItem(string clientID, int productID,int qauntity);
        public void RemoveItem(string clientID, int productID);
        public void IncreaseItemByOne(string clientID, int productID);
        public void DecreaseItemByOne(string clientID, int productID);
        public void ClearCart(string clientID);
    }
}
