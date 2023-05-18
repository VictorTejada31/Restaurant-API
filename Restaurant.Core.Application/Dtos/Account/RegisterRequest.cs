using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.Dtos.Account
{
    //<summary>
    // Parameters to create a new user.
    //</summary>
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Required")]
        [SwaggerParameter(
           Description = "First Name"
           )]
        //<example>
        // John 
        //</example>
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [SwaggerParameter(
           Description = "Last Name"
           )]
        //<example>
        // Doe 
        //</example>
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [SwaggerParameter(
           Description = "User Email"
           )]
        //<example>
        // johndoe@gmail.com
        //</example>
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [SwaggerParameter(
           Description = "User Password"
           )]
        //<example>
        // Pa$$0502
        //</example>
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [Compare(nameof(Password),ErrorMessage = "Passwords don't match")]
        [SwaggerParameter(
           Description = "Password Cofirmation"
           )]
        //<example>
        // Pa$$0502
        //</example>
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        [SwaggerParameter(
           Description = "Phone Number"
           )]
        //<example>
        // 000 000-000
        //</example>
        public string Phone { get; set; }

    }
}
