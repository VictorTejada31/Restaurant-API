using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Core.Application.Dtos.Account
{
    //<summary>
    // Parameters to authenticate.
    //</summary>
    public class AuthenticationRequest
    {
        [Required(ErrorMessage = "Required")]
        [SwaggerParameter(
           Description = "User Email"
           )]
        //<example>
        // user@gmail.com
        //</example>
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [SwaggerParameter(
           Description = "User Password"
           )]
        //<example>
        // Pa$$0510
        //</example>
        public string Password { get; set; }
    }
}
