using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Orders;
using Restaurant.Domain.Orders.Entities;
using Restaurant.Domain.Products;
using Restaurant.Domain.Products.Entities;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Entities;

namespace Restaurant.Infrastructure.Persistent;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<Role> Roles { get; set; } = null!;

    public DbSet<Permission> Permissions { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}