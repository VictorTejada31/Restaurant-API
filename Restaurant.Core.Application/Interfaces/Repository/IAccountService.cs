using Restaurant.Core.Application.Dtos.Account;


namespace Restaurant.Core.Application.Interfaces.Repository
{
    public interface IAccountService
    {
        Task<RegisterResponse> CreateAdminAsync(RegisterRequest request);
        Task<RegisterResponse> CreateWaiterAsync(RegisterRequest request);
        Task<AuthenticationResponse> SignInAsync(AuthenticationRequest request);
        Task SignOutAsync();
    }
}

