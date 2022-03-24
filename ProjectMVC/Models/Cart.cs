using System;

namespace ProjectMVC.Models
{  
    //cartProduct
    public class Cart
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
       public Product Product { get; set; }
        public string ClientId { get; set; }
        public DateTime dateTime { get; set; }
        public int Quntity { get; set; }
        public double TotalPrice { get; set; }

        public Client Client { get; set; }
    }
}
