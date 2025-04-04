using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;

namespace Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure composite key for OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            // Configure relationships
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Member)
                .WithMany(m => m.Orders)
                .HasForeignKey(o => o.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
    .HasOne(p => p.Category)
    .WithMany(c => c.Products)
    .HasForeignKey(p => p.CategoryId)
    .OnDelete(DeleteBehavior.Cascade);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Electronics",
                    Description = "Electronic devices and gadgets"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Home Appliances",
                    Description = "Appliances for home use"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Clothing",
                    Description = "Apparel and fashion items"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "Kitchen",
                    Description = "Kitchen tools and equipment"
                }
            );
            // Seed Members
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    MemberId = 1,
                    Email = "john.doe@example.com",
                    CompanyName = "ABC Corporation",
                    City = "New York",
                    Country = "USA",
                    Password = "P@ssw0rd1"
                },
                new Member
                {
                    MemberId = 2,
                    Email = "jane.smith@example.com",
                    CompanyName = "XYZ Ltd",
                    City = "London",
                    Country = "UK",
                    Password = "P@ssw0rd2"
                },
                new Member
                {
                    MemberId = 3,
                    Email = "nguyen.van@example.com",
                    CompanyName = "Tech Solutions",
                    City = "Hanoi",
                    Country = "Vietnam",
                    Password = "P@ssw0rd3"
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    CategoryId = 1,
                    ProductName = "Laptop Dell XPS 13",
                    Weight = "1.2 kg",
                    UnitPrice = 1299.99m,
                    UnitsInStock = 50
                },
                new Product
                {
                    ProductId = 2,
                    CategoryId = 1,
                    ProductName = "iPhone 15 Pro",
                    Weight = "0.2 kg",
                    UnitPrice = 999.99m,
                    UnitsInStock = 100
                },
                new Product
                {
                    ProductId = 3,
                    CategoryId = 2,
                    ProductName = "Samsung 4K TV",
                    Weight = "15 kg",
                    UnitPrice = 799.99m,
                    UnitsInStock = 30
                },
                new Product
                {
                    ProductId = 4,
                    CategoryId = 3,
                    ProductName = "Nike Air Max",
                    Weight = "0.3 kg",
                    UnitPrice = 129.99m,
                    UnitsInStock = 200
                },
                new Product
                {
                    ProductId = 5,
                    CategoryId = 4,
                    ProductName = "Coffee Maker",
                    Weight = "2.5 kg",
                    UnitPrice = 89.99m,
                    UnitsInStock = 45
                }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    MemberId = 1,
                    OrderDate = new DateTime(2023, 10, 15),
                    RequiredDate = new DateTime(2023, 10, 20),
                    ShippedDate = new DateTime(2023, 10, 18),
                    Freight = 15.50m
                },
                new Order
                {
                    OrderId = 2,
                    MemberId = 2,
                    OrderDate = new DateTime(2023, 11, 5),
                    RequiredDate = new DateTime(2023, 11, 10),
                    ShippedDate = null,
                    Freight = 12.75m
                },
                new Order
                {
                    OrderId = 3,
                    MemberId = 3,
                    OrderDate = new DateTime(2023, 11, 8),
                    RequiredDate = new DateTime(2023, 11, 15),
                    ShippedDate = new DateTime(2023, 11, 10),
                    Freight = 25.00m
                },
                new Order
                {
                    OrderId = 4,
                    MemberId = 1,
                    OrderDate = new DateTime(2023, 12, 1),
                    RequiredDate = new DateTime(2023, 12, 10),
                    ShippedDate = new DateTime(2023, 12, 3),
                    Freight = 10.25m
                }
            );

            // Seed OrderDetails
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 1,
                    UnitPrice = 1299.99m,
                    Quantity = 1,
                    Discount = 0.05f
                },
                new OrderDetail
                {
                    OrderId = 1,
                    ProductId = 5,
                    UnitPrice = 89.99m,
                    Quantity = 2,
                    Discount = 0.0f
                },
                new OrderDetail
                {
                    OrderId = 2,
                    ProductId = 3,
                    UnitPrice = 799.99m,
                    Quantity = 1,
                    Discount = 0.1f
                },
                new OrderDetail
                {
                    OrderId = 3,
                    ProductId = 2,
                    UnitPrice = 999.99m,
                    Quantity = 1,
                    Discount = 0.0f
                },
                new OrderDetail
                {
                    OrderId = 3,
                    ProductId = 4,
                    UnitPrice = 129.99m,
                    Quantity = 2,
                    Discount = 0.15f
                },
                new OrderDetail
                {
                    OrderId = 4,
                    ProductId = 5,
                    UnitPrice = 89.99m,
                    Quantity = 1,
                    Discount = 0.0f
                }
            );
        }
    }
}