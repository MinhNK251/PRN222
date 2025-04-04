﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Data;

#nullable disable

namespace Assignment3_Blazor.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250330074455_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Repository.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("MemberId");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            MemberId = 1,
                            City = "New York",
                            CompanyName = "ABC Corporation",
                            Country = "USA",
                            Email = "john.doe@example.com",
                            Password = "P@ssw0rd1"
                        },
                        new
                        {
                            MemberId = 2,
                            City = "London",
                            CompanyName = "XYZ Ltd",
                            Country = "UK",
                            Email = "jane.smith@example.com",
                            Password = "P@ssw0rd2"
                        },
                        new
                        {
                            MemberId = 3,
                            City = "Hanoi",
                            CompanyName = "Tech Solutions",
                            Country = "Vietnam",
                            Email = "nguyen.van@example.com",
                            Password = "P@ssw0rd3"
                        });
                });

            modelBuilder.Entity("Repository.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<decimal?>("Freight")
                        .HasColumnType("money");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("RequiredDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("MemberId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            Freight = 15.50m,
                            MemberId = 1,
                            OrderDate = new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RequiredDate = new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ShippedDate = new DateTime(2023, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            OrderId = 2,
                            Freight = 12.75m,
                            MemberId = 2,
                            OrderDate = new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RequiredDate = new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            OrderId = 3,
                            Freight = 25.00m,
                            MemberId = 3,
                            OrderDate = new DateTime(2023, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RequiredDate = new DateTime(2023, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ShippedDate = new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            OrderId = 4,
                            Freight = 10.25m,
                            MemberId = 1,
                            OrderDate = new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RequiredDate = new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ShippedDate = new DateTime(2023, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Repository.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<float>("Discount")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            ProductId = 1,
                            Discount = 0.05f,
                            Quantity = 1,
                            UnitPrice = 1299.99m
                        },
                        new
                        {
                            OrderId = 1,
                            ProductId = 5,
                            Discount = 0f,
                            Quantity = 2,
                            UnitPrice = 89.99m
                        },
                        new
                        {
                            OrderId = 2,
                            ProductId = 3,
                            Discount = 0.1f,
                            Quantity = 1,
                            UnitPrice = 799.99m
                        },
                        new
                        {
                            OrderId = 3,
                            ProductId = 2,
                            Discount = 0f,
                            Quantity = 1,
                            UnitPrice = 999.99m
                        },
                        new
                        {
                            OrderId = 3,
                            ProductId = 4,
                            Discount = 0.15f,
                            Quantity = 2,
                            UnitPrice = 129.99m
                        },
                        new
                        {
                            OrderId = 4,
                            ProductId = 5,
                            Discount = 0f,
                            Quantity = 1,
                            UnitPrice = 89.99m
                        });
                });

            modelBuilder.Entity("Repository.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.Property<int>("UnitsInStock")
                        .HasColumnType("int");

                    b.Property<string>("Weight")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            ProductName = "Laptop Dell XPS 13",
                            UnitPrice = 1299.99m,
                            UnitsInStock = 50,
                            Weight = "1.2 kg"
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            ProductName = "iPhone 15 Pro",
                            UnitPrice = 999.99m,
                            UnitsInStock = 100,
                            Weight = "0.2 kg"
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            ProductName = "Samsung 4K TV",
                            UnitPrice = 799.99m,
                            UnitsInStock = 30,
                            Weight = "15 kg"
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 3,
                            ProductName = "Nike Air Max",
                            UnitPrice = 129.99m,
                            UnitsInStock = 200,
                            Weight = "0.3 kg"
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 4,
                            ProductName = "Coffee Maker",
                            UnitPrice = 89.99m,
                            UnitsInStock = 45,
                            Weight = "2.5 kg"
                        });
                });

            modelBuilder.Entity("Repository.Models.Order", b =>
                {
                    b.HasOne("Repository.Models.Member", "Member")
                        .WithMany("Orders")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Repository.Models.OrderDetail", b =>
                {
                    b.HasOne("Repository.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Repository.Models.Member", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Repository.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Repository.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
