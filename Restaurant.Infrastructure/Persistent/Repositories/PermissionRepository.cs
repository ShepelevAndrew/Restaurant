using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Users.Entities;
using Restaurant.Domain.Users.Repositories;
using Serilog;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class PermissionRepository : IPermissionRepository
{
    private readonly RestaurantDbContext _dbContext;
    private readonly ILogger _logger;

    public PermissionRepository(RestaurantDbContext dbContext, ILogger logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Permission>> Get()
    {
        FormattableString query = $"SELECT * FROM public.\"Permissions\"";
        try
        {
            var permissions = await _dbContext.Permissions
                .FromSql(query)
                .ToListAsync();

            return permissions;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in PermissionRepository.");
            throw;
        }
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByIds(params int[] permissionIds)
    {
        FormattableString query = $"SELECT * FROM public.\"Permissions\"";
        try
        {
            var permissions = await _dbContext.Permissions
                .FromSql(query)
                .Where(p => permissionIds.Any(pIds => p.Id == pIds))
                .ToListAsync();

            return permissions;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error with '{query}' sql query in PermissionRepository.");
            throw;
        }
    }

    public async Task<HashSet<string>> GetPermissionNames(Guid userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user is null)
            return new HashSet<string>();

        var roles = await _dbContext.Roles
            .Include(r => r.Permissions)
            .Where(r => r.Id == user.RoleId)
            .ToArrayAsync();

        return roles
            .SelectMany(r => r.Permissions)
            .Select(p => p.Name)
            .ToHashSet();
    }
}