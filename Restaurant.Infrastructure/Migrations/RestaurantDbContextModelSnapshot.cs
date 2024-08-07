﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Restaurant.Infrastructure.Persistent;

#nullable disable

namespace Restaurant.Infrastructure.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    partial class RestaurantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.3.24172.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Restaurant.Domain.Orders.Entities.OrderDetail", b =>
                {
                    b.Property<Guid>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Restaurant.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("BuyDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CancelledDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ConfirmedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("CookedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("PaidDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("Sum")
                        .HasColumnType("numeric");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Restaurant.Domain.Products.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("CategoryId");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Restaurant.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<long>("Weight")
                        .HasColumnType("bigint");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Restaurant.Domain.Users.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Read"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Create"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Update"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Delete"
                        },
                        new
                        {
                            Id = 5,
                            Name = "GetBoughtOrders"
                        },
                        new
                        {
                            Id = 6,
                            Name = "BuyOrder"
                        },
                        new
                        {
                            Id = 7,
                            Name = "GetVerifiedOrders"
                        },
                        new
                        {
                            Id = 8,
                            Name = "VerifyOrder"
                        },
                        new
                        {
                            Id = 9,
                            Name = "ShippedOrder"
                        },
                        new
                        {
                            Id = 10,
                            Name = "GetPaidOrders"
                        },
                        new
                        {
                            Id = 11,
                            Name = "PaidOrder"
                        },
                        new
                        {
                            Id = 12,
                            Name = "GetCancelledOrders"
                        },
                        new
                        {
                            Id = 13,
                            Name = "CancelledOrder"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Cart"
                        },
                        new
                        {
                            Id = 15,
                            Name = "ReadUsers"
                        },
                        new
                        {
                            Id = 16,
                            Name = "UpdateMyUser"
                        },
                        new
                        {
                            Id = 17,
                            Name = "UpdateUsers"
                        },
                        new
                        {
                            Id = 18,
                            Name = "DeleteUsers"
                        },
                        new
                        {
                            Id = 19,
                            Name = "CreateRole"
                        });
                });

            modelBuilder.Entity("Restaurant.Domain.Users.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Owner"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Operator"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Courier"
                        },
                        new
                        {
                            Id = 6,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("Restaurant.Domain.Users.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsEmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("4c916310-80ab-4f2d-a097-eed199803058"),
                            Email = "owner@gmail.com",
                            Firstname = "name",
                            IsEmailConfirmed = true,
                            Lastname = "lastname",
                            Password = "3302df19c4918e8271ef446321e66f3abdc0defe3d28cdf3798d9ddd7fb1a7eb",
                            Phone = "+380698432576",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Restaurant.Infrastructure.Persistent.ModelConfiguration.Entities.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 15
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 17
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 18
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 19
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 15
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 2,
                            PermissionId = 17
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 15
                        },
                        new
                        {
                            RoleId = 3,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 15
                        },
                        new
                        {
                            RoleId = 4,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 11
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 5,
                            PermissionId = 16
                        },
                        new
                        {
                            RoleId = 6,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 6,
                            PermissionId = 14
                        },
                        new
                        {
                            RoleId = 6,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 6,
                            PermissionId = 16
                        });
                });

            modelBuilder.Entity("Restaurant.Domain.Orders.Entities.OrderDetail", b =>
                {
                    b.HasOne("Restaurant.Domain.Orders.Order", null)
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Restaurant.Domain.Products.Entities.Category", b =>
                {
                    b.HasOne("Restaurant.Domain.Products.Entities.Category", null)
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Restaurant.Domain.Products.Product", b =>
                {
                    b.HasOne("Restaurant.Domain.Products.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Restaurant.Domain.Products.ValueObjects.AverageRating", "AverageRating", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.Property<long>("NumRatings")
                                .HasColumnType("bigint");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("AverageRating")
                        .IsRequired();
                });

            modelBuilder.Entity("Restaurant.Domain.Users.User", b =>
                {
                    b.HasOne("Restaurant.Domain.Users.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Restaurant.Infrastructure.Persistent.ModelConfiguration.Entities.RolePermission", b =>
                {
                    b.HasOne("Restaurant.Domain.Users.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Restaurant.Domain.Users.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Restaurant.Domain.Orders.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Restaurant.Domain.Products.Entities.Category", b =>
                {
                    b.Navigation("SubCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
