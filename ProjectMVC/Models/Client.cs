

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMVC.Models
{
    public class Client
    //{[Foreignkey]
    {
        [Key]
        [ForeignKey("user")]
        public string Id { get; set; }
        [Required]
        [Display(Name = "User Name")]
        [RegularExpression(pattern: @"[a-zA-z]{3,}", ErrorMessage = "your name must be more than 3 char")]
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "Exsist", controller: "Account",
           ErrorMessage = "this email already exsist")]
        public string  Email { get; set; }  
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm not matched")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        IdentityUser user { get; set; }
        public ICollection <Cart> carts { get; set; } // 1  to 1 
        public ICollection<Order> orders { get; set; }



    }
    public enum Gender
    {
        Male, Female
    }
}
