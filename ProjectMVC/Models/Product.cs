using System.Collections.Generic;

namespace ProjectMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public double Price { get; set; }
        public int NumInStock { get; set; }
        public string ImgPath   { get; set; }
        public int CategoryId { get; set; } 
      public  Category Category { get; set; }
        public ICollection<Cart> carts { get; set; }
        public ICollection<OrderProduct> orderProducts { get; set; }



    }
}
