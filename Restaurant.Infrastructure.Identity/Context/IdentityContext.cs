using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Infrastructure.Identity.Entities;

namespace Restaurant.Infrastructure.Identity.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");

            builder.Entity<ApplicationUser>(options =>
            {
                options.ToTable("Users");
            });

            builder.Entity<IdentityRole>(options =>
            {
                options.ToTable("Roles");
            });

            builder.Entity<IdentityUserLogin<string>>(options =>
            {
                options.ToTable("UserLogin");
            });

            builder.Entity<IdentityUserRole<string>>(options =>
            {
                options.ToTable("UserRole");
            });

        }
    }
}
