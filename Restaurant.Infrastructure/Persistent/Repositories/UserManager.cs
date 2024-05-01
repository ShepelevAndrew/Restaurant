using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Restaurant.Application.Abstractions.Users;
using Restaurant.Application.Abstractions.VerificationCode;
using Restaurant.Domain.Users;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class UserManager : UserRepository, IUserManager
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IDistributedCache _distributedCache;
    private readonly ICodeGenerator _codeGenerator;

    public UserManager(
        RestaurantDbContext dbContext,
        IDistributedCache distributedCache,
        ICodeGenerator codeGenerator) : base(dbContext)
    {
        _dbContext = dbContext;
        _distributedCache = distributedCache;
        _codeGenerator = codeGenerator;
    }

    public async Task<string> GenerateCode(User user)
    {
        var expirationTimeForCode = TimeSpan.FromHours(1);
        var code = _codeGenerator.GenerateCode();

        await _distributedCache.SetAsync(
            user.Email,
            Encoding.UTF8.GetBytes(code),
            new DistributedCacheEntryOptions().SetAbsoluteExpiration(expirationTimeForCode));

        return code;
    }

    public async Task<bool> ConfirmEmail(User user, string code)
    {
        var existingCode = await _distributedCache.GetStringAsync(user.Email);
        if (!code.Equals(existingCode))
        {
            return false;
        }

        user.ConfirmEmail();
        await _dbContext.SaveChangesAsync();

        return true;
    }
}