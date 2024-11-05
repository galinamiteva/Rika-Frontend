using RikaWebApp.Helpers;
using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.Models.Auth
{
    public class SignUpModel
    {
        //[Required(ErrorMessage = "A name is required")]
        //[Display(Order = 0)]
        //public string FirstName { get; set; } = null!;

        //[Required(ErrorMessage = "A name is required")]
        //[Display(Order = 1)]
        //public string LastName { get; set; } = null!;

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

        //[Required(ErrorMessage = "You must confirm your password")]
        //[Display(Order = 4)]
        //[DataType(DataType.Password)]
        //[Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
        //public string ConfirmPassword { get; set; } = null!;

        //[Display(Order = 5)]
        //[CheckBoxRequired(ErrorMessage = "You must agree to our terms and condition")]
        //public bool TermsAndConditions { get; set; } = false;

        [DataType(DataType.Text)]
        [Display(Name = "First Name", Prompt = "Enter your first name", Order = 0)]
        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters long")]
        public string FirstName { get; set; } = null!;

        [DataType(DataType.Text)]
        [Display(Name = "Last Name", Prompt = "Enter your last name", Order = 1)]
        [Required(ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long")]
        public string LastName { get; set; } = null!;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter your email", Order = 2)]
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^(?=.*[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,})[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
        ErrorMessage = "The email format is not valid.")]
        public string Email { get; set; } = null!;

        [Display(Name = "Password", Prompt = "********", Order = 3)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "At least one lowercase, uppercase letter and  one special character.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s])(?=.*[a-zA-Z\d]).{8,}$",
        ErrorMessage = " At least one lowercase, uppercase letter and  one special character.")]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirm Password", Prompt = "********", Order = 4)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password must be comfirmd")]
        [Compare("Password", ErrorMessage = "The confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;

        [Display(Name = "Terms & Conditions ", Order = 5)]
        [CheckBoxRequired(ErrorMessage = "Terms & Conditions is required field ")]
        public bool TermsAndConditions { get; set; } = false;
    }

    public class CheckBoxRequired : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value is bool b && b;
        }
    }
}
