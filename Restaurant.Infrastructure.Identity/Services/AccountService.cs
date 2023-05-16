using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Core.Application.Enums;
using Restaurant.Core.Application.Interfaces.Repository;
using Restaurant.Core.Domain.Settings;
using Restaurant.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Restaurant.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jWTSettings;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTSettings = options.Value;
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
                var securityToken = await GenerateJWToken(account);
                response.RefreshToken = GenerateRefreshToken().Token;
                response.Token = new JwtSecurityTokenHandler().WriteToken(securityToken);

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

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            foreach(var userClaim in userClaims)
            {
                roleClaims.Add(new Claim("roles", userClaim.ToString()));
            };

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Email, user.Email),
              new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
              new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
              new Claim("UserId", user.Id)

            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                  audience: _jWTSettings.Audience,
                  issuer: _jWTSettings.Issuer,
                  expires: DateTime.UtcNow.AddMinutes(_jWTSettings.DurationInMinues),
                  signingCredentials: signingCredentials,
                  claims: claims
                );
         



            return token; 
        }
        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                Token = GenerateRandomToken()
            };
        }

        private string GenerateRandomToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            Byte[] bytesAray = new Byte[40];
            rngCryptoServiceProvider.GetBytes(bytesAray);

            return BitConverter.ToString(bytesAray).Replace("-","");


        }
        #endregion
    }
}
