using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Restaurant.Application.Common.Abstractions.Users;
using Restaurant.Application.Common.Abstractions.Auth.VerificationCode;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Infrastructure.Persistent.Repositories;

public class UserManager : IUserManager
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IDistributedCache _distributedCache;
    private readonly ICodeGenerator _codeGenerator;

    public UserManager(
        RestaurantDbContext dbContext,
        IUserRepository userRepository,
        IDistributedCache distributedCache,
        ICodeGenerator codeGenerator)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
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

    public async Task<bool> Create(User user) => await _userRepository.Create(user);

    public async Task<bool> Update(User user) => await _userRepository.Update(user);

    public async Task<IEnumerable<User>> Get() => await _userRepository.Get();

    public async Task<User?> GetById(Guid userId) => await _userRepository.GetById(userId);

    public async Task<User?> GetByEmail(string email) => await _userRepository.GetByEmail(email);

    public async Task<bool> Delete(string email) => await _userRepository.Delete(email);

    public async Task<bool> IsEmailExist(string email) => await _userRepository.IsEmailExist(email);
}