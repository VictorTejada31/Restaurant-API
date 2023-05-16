using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Core.Application.Interfaces.Repository;

namespace Restaurant.WebApi.Controllers
{
    [ApiController]
    [Route("api/{version:apiVersion}/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(200,Type = typeof(AuthenticationResponse))]
        [ProducesResponseType(500)]
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
