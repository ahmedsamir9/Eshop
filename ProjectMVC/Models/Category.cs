using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Remote(controller: "Categories", action: "Exsist", ErrorMessage = "this Category already exsist" , AdditionalFields = "Id")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public ICollection <Product> Products { get; set; }

    }
}
