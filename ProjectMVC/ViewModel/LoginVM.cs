using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Username is required!")]
        [Display(Name = "Username")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
