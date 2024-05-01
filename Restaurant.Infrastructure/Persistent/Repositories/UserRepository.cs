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

    public async Task<bool> Create(User user)
    {
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(
                $"INSERT INTO public.\"Users\" (\"UserId\", \"Firstname\", \"Lastname\", \"Email\", \"Phone\", \"Password\") VALUES ({user.UserId}, {user.Firstname}, {user.Lastname}, {user.Email}, {user.Phone}, {user.Password})");

            return rawsCount > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Update(User user)
    {
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(
                $"UPDATE public.\"Users\" SET \"Firstname\" = {user.Firstname}, \"Lastname\" = {user.Lastname}, \"Phone\" = {user.Phone} WHERE \"Email\" = {user.Email}");

            return rawsCount > 0;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<User>> Get()
    {
        var users = await _dbContext.Users
            .FromSql($"SELECT * FROM public.\"Users\"")
            .ToListAsync();

        return users;
    }

    public async Task<bool> Delete(string email)
    {
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(
                $"DELETE FROM public.\"Users\" WHERE \"Email\" = {email}");

            return rawsCount > 0;
        }
        catch
        {
            return false;
        }
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