using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models.Auth
{
    public class ValidateModel
    {
        [Required(ErrorMessage = "An email is required")]
        [Display(Order = 2)]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        public string Code { get; set; } = null!;
    }
}
