using System;

namespace ProjectMVC.Models
{  
    //cartProduct
    public class Cart
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        public string ClientId { get; set; }
        public DateTime dateTime { get; set; }
        public int Quntity { get; set; }
        public double TotalPrice { get; set; }
        public virtual Product Product { get; set; }

        public virtual Client Client { get; set; }
    }
}
