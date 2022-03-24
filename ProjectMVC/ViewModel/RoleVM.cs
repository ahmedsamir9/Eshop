using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.ViewModel
{
    public class RoleVM
    {
        [Required]
        [Display(Name = "Role Name")]
        [Remote(controller: "Role", action: "Exsist", ErrorMessage = "this role already exsist")]
        public string Name { get; set; }
    }
}
