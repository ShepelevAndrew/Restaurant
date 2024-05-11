using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Users.Entities;
using Restaurant.Domain.Users.Repositories;
using Serilog;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RestaurantDbContext _dbContext;
    private readonly ILogger _logger;

    public RoleRepository(RestaurantDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<bool> Create(Role role)
    {
        FormattableString command =
            $"INSERT INTO public.\"Roles\" (\"Id\", \"Name\") VALUES ({role.Id}, {role.Name})";

        var commandsRolePermission = new List<FormattableString>();
        foreach (var permission in role.Permissions)
        {
            commandsRolePermission.Add($"INSERT INTO public.\"RolePermission\" (\"RoleId\", \"PermissionId\") VALUES ({role.Id}, {permission.Id})");
        }

        try
        {
            var rawsCount = await _dbContext.Database.ExecuteSqlAsync(command);
            if (rawsCount <= 0)
            {
                return false;
            }

            foreach (var commandRolePermission in commandsRolePermission)
            {
                rawsCount = await _dbContext.Database.ExecuteSqlAsync(commandRolePermission);
                if (rawsCount <= 0)
                {
                    return false;
                }
            }

            return true;
        }
        catch(Exception ex)
        {
            _logger.Error(ex, $"Error with '{command}' sql command in ProductRepository.");
            return false;
        }
    }

    public async Task<IEnumerable<Role>> Get()
    {
        FormattableString query = $"SELECT * FROM public.\"Roles\"";
        try
        {
            var role = await _dbContext.Roles
                .FromSql(query)
                .Include(r => r.Permissions)
                .ToListAsync();

            return role;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in RoleRepository.");
            throw;
        }
    }

    public async Task<Role?> GetById(int roleId)
    {
        FormattableString query = $"SELECT * FROM public.\"Roles\" WHERE \"Id\" = {roleId}";
        try
        {
            var role = await _dbContext.Roles
                .FromSql(query)
                .Include(r => r.Permissions)
                .SingleOrDefaultAsync();

            return role;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in RoleRepository.");
            throw;
        }
    }

    public async Task<int> GenerateId()
    {
        FormattableString query = $"SELECT * FROM public.\"Roles\" ORDER BY \"Id\" DESC LIMIT 1";
        try
        {
            var role = await _dbContext.Roles
                .FromSql(query)
                .SingleAsync();

            return role.Id;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in RoleRepository.");
            throw;
        }
    }

    public async Task<bool> IsExist(int roleId)
    {
        FormattableString query = $"SELECT * FROM public.\"Roles\" WHERE \"Id\" = {roleId}";
        try
        {
            var role = await _dbContext.Roles
                .FromSql(query)
                .SingleOrDefaultAsync();

            return role is not null;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in RoleRepository.");
            throw;
        }
    }
}