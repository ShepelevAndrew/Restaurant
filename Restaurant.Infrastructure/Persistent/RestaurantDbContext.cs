using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Users;

namespace Restaurant.Infrastructure.Persistent;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
}