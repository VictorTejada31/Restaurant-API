using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Seeds
{
    public static class DefaultWaiterUser
    {
        public static async Task AddAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser user = new()
            {

                FirstName = "John",
                LastName = "Doe",
                Email = "waiter@restaurant.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };

            if (userManager.Users.All(u => u.Id != user.Id))
            {
                await userManager.CreateAsync(user, "Pa$$1234");
                await userManager.AddToRoleAsync(user, Roles.Waiter.ToString());
            }

        }

    }
}
