using System;
using System.Collections.Generic;

namespace ProjectMVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string ClientId { get; set; }
        public DateTime dateTime { get; set; }
      
        public double TotalPrice { get; set; }

        public Client Client { get; set; }
        public ICollection<OrderProduct> orderProducts { get; set; }
    }
}
