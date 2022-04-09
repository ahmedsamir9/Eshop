using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMVC.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int NumInStock { get; set; }

        public string ImgPath   { get; set; }

        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        public int CategoryId { get; set; } 
        public  Category Category { get; set; }
        public ICollection<Cart> carts { get; set; }
        public ICollection<OrderProduct> orderProducts { get; set; }



    }
}
