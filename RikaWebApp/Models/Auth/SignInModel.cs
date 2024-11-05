using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models.Auth;

public class SignInModel
{


    //[Required(ErrorMessage = "An email is required")]
    //[Display(Order = 2)]
    //[EmailAddress]
    //[RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]
    //public string Email { get; set; } = null!;

    //[Required(ErrorMessage = "A password is required")]
    //[Display(Order = 3)]
    //[DataType(DataType.Password)]
    //[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()\\-_=+{};:,<.>]).{8,}$", ErrorMessage = "Invalid password")]
    //public string Password { get; set; } = null!;

    [Display(Name = "Email", Prompt = "Enter your email", Order = 0)]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Email is required")]

    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "********", Order = 1)]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Password is required")]

    public string Password { get; set; } = null!;

}
