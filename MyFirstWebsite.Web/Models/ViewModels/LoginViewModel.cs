using System.ComponentModel.DataAnnotations;

namespace MyFirstWebsite.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage ="Minimum of 8 characters")]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
