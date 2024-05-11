using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;
using Serilog;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RestaurantDbContext _dbContext;
    private readonly ILogger _logger;

    public UserRepository(RestaurantDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> Create(User user)
    {
        FormattableString command =
            $"INSERT INTO public.\"Users\" (\"UserId\", \"Firstname\", \"Lastname\", \"Email\", \"Phone\", \"Password\", \"RoleId\") VALUES ({user.UserId}, {user.Firstname}, {user.Lastname}, {user.Email}, {user.Phone}, {user.Password}, {user.RoleId})";  
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in UserRepository.");
            return false;
        }
    }

    public async Task<bool> Update(User user)
    {
        FormattableString command =
            $"UPDATE public.\"Users\" SET \"Firstname\" = {user.Firstname}, \"Lastname\" = {user.Lastname}, \"Phone\" = {user.Phone}, \"RoleId\" = {user.RoleId} WHERE \"Email\" = {user.Email}";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in UserRepository.");
            return false;
        }
    }

    public async Task<IEnumerable<User>> Get()
    {
        FormattableString query = $"SELECT * FROM public.\"Users\"";
        try
        {
            var users = await _dbContext.Users
                .FromSql(query)
                .ToListAsync();

            return users;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in UserRepository.");
            throw;
        }
    }

    public async Task<User?> GetById(Guid userId)
    {
        FormattableString query = $"SELECT * FROM public.\"Users\" WHERE \"UserId\" = {userId}";
        try
        {
            var user = await _dbContext.Users
                .FromSql(query)
                .SingleOrDefaultAsync();

            return user;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in UserRepository.");
            throw;
        }
    }

    public async Task<User?> GetByEmail(string email)
    {
        FormattableString query = $"SELECT * FROM public.\"Users\" WHERE \"Email\" = {email}";
        try
        {
            var user = await _dbContext.Users
                .FromSql(query)
                .SingleOrDefaultAsync();

            return user;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in UserRepository.");
            throw;
        }
    }

    public async Task<bool> Delete(string email)
    {
        FormattableString command = $"DELETE FROM public.\"Users\" WHERE \"Email\" = {email}";
        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);

            return rawsCount > 0;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in UserRepository.");
            return false;
        }
    }

    public async Task<bool> IsEmailExist(string email)
    {
        FormattableString query = $"SELECT * FROM public.\"Users\" WHERE \"Email\" = {email}";
        try
        {
            var user = await _dbContext.Users
                .FromSql(query)
                .SingleOrDefaultAsync();

            return user is not null;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in UserRepository.");
            throw;
        }
    }
}