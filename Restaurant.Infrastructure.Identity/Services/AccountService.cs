using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Async Methods

        public async Task<AuthenticationResponse> SignInAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse { HasError = false };
            ApplicationUser account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"{request.Email} is not registed.";
                return response;
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(account, request.Password, isPersistent: true, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    response.HasError = true;
                    response.Error = $"Wrong Password";
                    return response;
                }

                response.FirstName = account.FirstName;
                response.LastName = account.LastName;
                response.Email = account.Email;
                response.Id = account.Id;
                IList<string> roles = await _userManager.GetRolesAsync(account);
                response.Roles = roles;

                return response;

            }

        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<RegisterResponse> CreateWaiterAsync(RegisterRequest request)
        {
            RegisterResponse response = new() { HasError = false };
            ApplicationUser account = await _userManager.FindByEmailAsync(request.Email);

            if (account != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already in used";
                return response;
            }

            ApplicationUser newWaiter = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.Phone,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(newWaiter, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Error while creating Waiter";
                return response;
            }

            await _userManager.AddToRoleAsync(newWaiter, Roles.Waiter.ToString());

            return response;
        }
        public async Task<RegisterResponse> CreateAdminAsync(RegisterRequest request)
        {
            RegisterResponse response = new() { HasError = false };
            ApplicationUser account = await _userManager.FindByEmailAsync(request.Email);

            if (account != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already in used";
                return response;
            }

            ApplicationUser newAdmin = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.Phone,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(newAdmin, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Error while creating Admin";
                return response;
            }

            await _userManager.AddToRoleAsync(newAdmin, Roles.Admin.ToString());

            return response;
        }

        #endregion

        #region Privete Methods

         

        #endregion
    }
}
