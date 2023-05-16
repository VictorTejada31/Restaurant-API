using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Domain.Entities;

namespace Restaurant.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tables> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tables

            modelBuilder.Entity<Dish>().ToTable("Dishes");
            modelBuilder.Entity<DishCategory>().ToTable("DishCategories");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<Tables>().ToTable("Tables");


            #endregion

            #region Primary Key

            modelBuilder.Entity<Dish>().HasKey(d => d.Id);
            modelBuilder.Entity<DishCategory>().HasKey(d => d.Id);
            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
            modelBuilder.Entity<Tables>().HasKey(t => t.Id);


            #endregion

            #region Relations

            modelBuilder.Entity<DishCategory>()
                .HasMany<Dish>(d => d.Dishes)
                .WithOne(d => d.DishCategory)
                .HasForeignKey(d => d.DishCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tables>()
                .HasMany<Order>(t => t.Orders)
                .WithOne(o => o.Table)
                .HasForeignKey(t => t.TableId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region Property Configuration

            #region Dish

            modelBuilder.Entity<Dish>().Property(d => d.Ingredients)
                .HasMaxLength(150)
                .IsRequired();

            modelBuilder.Entity<Dish>().Property(d => d.Price)
                .IsRequired();

            modelBuilder.Entity<Dish>().Property(d => d.Name)
                .HasMaxLength(120)
                .IsRequired();

            modelBuilder.Entity<Dish>().Property(d => d.PeopleAmount)
                .IsRequired();

            #endregion

            #region DishCategory

            modelBuilder.Entity<DishCategory>().Property(d => d.Name)
                .HasMaxLength(80)
                .IsRequired();

            #endregion

            #region Ingredient

            modelBuilder.Entity<Ingredient>().Property(i => i.Name)
                .HasMaxLength(80)
                .IsRequired();

            #endregion

            #region Order

            modelBuilder.Entity<Order>().Property(o => o.Status)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Order>().Property(o => o.SubTotal)
                .IsRequired();

            modelBuilder.Entity<Order>().Property(o => o.Dishes)
                .HasMaxLength(150)
                .IsRequired();


            #endregion

            #region Table

            modelBuilder.Entity<Tables>().Property(t => t.Status)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Tables>().Property(t => t.Capacity)
                .IsRequired();

            modelBuilder.Entity<Tables>().Property(o => o.Description)
                .HasMaxLength(250)
                .IsRequired();


            #endregion

            #endregion
        }




    }
}
