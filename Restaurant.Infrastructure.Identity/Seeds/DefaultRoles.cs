using Microsoft.AspNetCore.Identity;
using Restaurant.Core.Application.Enums;

namespace Restaurant.Infrastructure.Identity.Seeds
{
    public class DefaultRoles 
    {
        public static async Task AddAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Waiter.ToString()));

        }
    }
}
