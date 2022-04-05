using System.ComponentModel.DataAnnotations;

namespace ProjectMVC.ViewModel
{
    public class ForgotPasswordVM
    {[Required]
    [EmailAddress]
     public string Email { get; set; }   

    }
}
