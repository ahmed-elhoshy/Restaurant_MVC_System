using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resturant.Models;

namespace Resturant.Data
{
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {   
            base.OnModelCreating(builder);
            // Configure the many-to-many relationship between Product and Ingredient
            builder.Entity<Ingredient>()
        .HasKey(i => i.IngredientId);
            builder.Entity<ProductIngredient>()
                .HasKey(pi => new { pi.ProductId, pi.IngredientId });
            builder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductIngredients)
                .HasForeignKey(pi => pi.ProductId);
            builder.Entity<ProductIngredient>()
                .HasOne(pi => pi.Ingredient)
                .WithMany(i => i.ProductIngredients)
                .HasForeignKey(pi => pi.IngredientId);
            // el productIngredient 3ndo 2 pk 2 fk => pID , IngID f ktbna el foo2 dah

            //seed data
            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Appetizers" },
                new Category { CategoryId = 2, Name = "Entree" },
                new Category { CategoryId = 3, Name = "Desserts" },
                new Category { CategoryId = 4, Name = "Beverages" },
                new Category { CategoryId = 5, Name = "Side Dish" }
            );

            builder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientId = 1, Name = "Chicken" },
                new Ingredient { IngredientId = 2, Name = "Beef" },
                new Ingredient { IngredientId = 3, Name = "Fish" },
                new Ingredient { IngredientId = 4, Name = "Tortilla" },
                new Ingredient { IngredientId = 5, Name = "Lettuce" },
                new Ingredient { IngredientId = 6, Name = "Tomato" },
                new Ingredient { IngredientId = 7, Name = "Cucumber" }
            );

            builder.Entity<Product>().HasData(
                new Product { ProductId = 1, CategoryId = 1, Name = "Spring Rolls", Description = "Crispy rolls filled with vegetables", Price = 5.99m, Stock = 10, ImageUrl = "https://example.com/springrolls.jpg" },
                new Product { ProductId = 2, CategoryId = 2, Name = "Grilled Chicken", Description = "Juicy grilled chicken with herbs", Price = 12.99m, Stock = 20, ImageUrl = "https://example.com/grilledchicken.jpg" },
                new Product { ProductId = 3, CategoryId = 3, Name = "Chocolate Cake", Description = "Rich chocolate cake with frosting", Price = 4.99m, Stock = 15, ImageUrl = "https://example.com/chocolatecake.jpg" },
                new Product { ProductId = 4, CategoryId = 4, Name = "Soda", Description = "Refreshing carbonated drink", Price = 1.99m, Stock = 50, ImageUrl = "https://example.com/soda.jpg" },
                new Product { ProductId = 5, CategoryId = 5, Name = "French Fries", Description = "Crispy golden fries", Price = 2.99m, Stock = 30, ImageUrl = "https://example.com/frenchfries.jpg" },
                new Product { ProductId = 6, CategoryId = 1, Name = "Caesar Salad", Description = "Fresh salad with Caesar dressing", Price = 6.99m, Stock = 25, ImageUrl = "https://example.com/caesarsalad.jpg" }
                );

            builder.Entity<ProductIngredient>().HasData(
                new ProductIngredient { ProductId = 1, IngredientId = 5 }, // Spring Rolls - Lettuce
                new ProductIngredient { ProductId = 1, IngredientId = 6 }, // Spring Rolls - Tomato
                new ProductIngredient { ProductId = 2, IngredientId = 1 }, // Grilled Chicken - Chicken
                new ProductIngredient { ProductId = 3, IngredientId = 7 }, // Chocolate Cake - Cucumber
                new ProductIngredient { ProductId = 4, IngredientId = 2 }, // Soda - Beef
                new ProductIngredient { ProductId = 5, IngredientId = 3 }  // French Fries - Fish

            );






        }
    }
}
