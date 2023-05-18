using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Core.Application.Interfaces.Repository;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("api/{version:apiVersion}/[Controller]")]
    [SwaggerTag("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(200,Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(500)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Log In",
            Description = "Receives the necessary parameters to log in."
            )]
        public async Task<IActionResult> Authentication(AuthenticationRequest request)
        {
            try
            {
                AuthenticationResponse response = await _accountService.SignInAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateAdmin")]
        [ProducesResponseType(200, Type = typeof(RegisterResponse))]
        [ProducesResponseType(500)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Create Admin",
            Description = "Receives the necessary parameters to create a new admin."
            )]
        public async Task<IActionResult> CreateAdmin(RegisterRequest request)
        {
            try
            {
                RegisterResponse response = await _accountService.CreateAdminAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateWaiter")]
        [ProducesResponseType(200, Type = typeof(RegisterResponse))]
        [ProducesResponseType(500)]
        [Consumes(MediaTypeNames.Application.Json)]
        [SwaggerOperation(
            Summary = "Create Waiter",
            Description = "Receives the necessary parameters to create a new waiter."
            )]
        public async Task<IActionResult> CreateWaiter(RegisterRequest request)
        {
            try
            {
                RegisterResponse response = await _accountService.CreateWaiterAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
