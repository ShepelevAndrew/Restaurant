using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RestaurantDbContext _dbContext;

    public UserRepository(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Create(User user)
    {
        await _dbContext.Database.ExecuteSqlAsync(
            $"INSERT INTO public.\"Users\" (\"UserId\", \"Firstname\", \"Lastname\", \"Email\", \"Phone\", \"Password\") VALUES ({user.UserId}, {user.Firstname}, {user.Lastname}, {user.Email}, {user.Phone}, {user.Password})");
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var user = await _dbContext.Users
            .FromSql($"SELECT * FROM public.\"Users\" WHERE \"Email\" = {email}")
            .SingleOrDefaultAsync();

        return user;
    }

    public async Task<bool> IsEmailExist(string email)
    {
        var user = await _dbContext.Users
            .FromSql($"SELECT * FROM public.\"Users\" WHERE \"Email\" = {email}")
            .SingleOrDefaultAsync();

        return user is not null;
    }
}