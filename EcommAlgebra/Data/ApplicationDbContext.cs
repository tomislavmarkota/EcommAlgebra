using EcommAlgebra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EcommAlgebra.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Editor", NormalizedName = "EDITOR" },
                new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "User", NormalizedName = "USER" }
                );


            builder.Entity<Product>().HasData(
                new Product() {Id= 1, Title = "Samsung X", Price = 400.99M, Description = "The best mobile phone on the market", ImageName = "mobile1.jpg" },
                new Product() { Id = 2, Title = "Iphone 10", Price = 800.99M, Description = "The best Iphone on the market", ImageName = "mobile2.jpg" },
                  new Product() { Id = 3, Title = "Laptop X", Price = 1999.99M, Description = "The best laptop on the market", ImageName = "laptop1.jpg" },
                   new Product() { Id = 4, Title = "Latop M", Price = 799.99M, Description = "The best laptop on the market", ImageName = "laptop2.jpg" },
                    new Product() { Id = 5, Title = "Latop L", Price = 899.99M, Description = "The best laptop on the market", ImageName = "laptop3.jpg" },
                    new Product() { Id = 6, Title = "Latop S", Price = 699.99M, Description = "The best laptop on the market", ImageName = "laptop4.jpg" },
                    new Product() { Id = 7, Title = "Latop PRO", Price = 999.99M, Description = "The best laptop on the market", ImageName = "laptop5.jpg" },
                    new Product() { Id = 8, Title = "Nokia", Price = 333.99M, Description = "The best mobile phone on the market", ImageName = "mobile3.jpg" },
                    new Product() { Id = 9, Title = "Siemens", Price = 111.99M, Description = "The best mobile phone on the market", ImageName = "mobile4.jpg" },
                    new Product() { Id = 10, Title = "Smartphone 11", Price = 999.99M, Description = "The best mobile phone on the market", ImageName = "mobile5.jpg" }
                );


            builder.Entity<Category>().HasData(
                new Category()
                { Id = 1, Title = "mobile phones" },
                new Category()
                { Id = 2, Title = "laptops" }
                );

            builder.Entity<ProductCategory>().HasData(
                new ProductCategory() { Id = 1, CategoryId = 1, ProductId = 1 },
                new ProductCategory() { Id = 2, CategoryId = 1, ProductId = 2 },
                new ProductCategory() { Id = 3, CategoryId = 2, ProductId = 3 },
                new ProductCategory() { Id = 4, CategoryId = 2, ProductId = 4 },
                new ProductCategory() { Id = 5, CategoryId = 2, ProductId = 5 },
                new ProductCategory() { Id = 6, CategoryId = 2, ProductId = 6 },
                new ProductCategory() { Id = 7, CategoryId = 2, ProductId = 7 },
                new ProductCategory() { Id = 8, CategoryId = 1, ProductId = 8 },
                new ProductCategory() { Id = 9, CategoryId = 1, ProductId = 9 },
                new ProductCategory() { Id = 10, CategoryId = 1, ProductId = 10 }
                );

              

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
       

    }
}