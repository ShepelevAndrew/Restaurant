using Microsoft.AspNetCore.Authorization;
using Restaurant.Domain.Users.Enums;

namespace Restaurant.Infrastructure.Auth.Authorization.Attributes;

public class HasPermission : AuthorizeAttribute
{
    public HasPermission(Permissions permission)
        : base(permission.ToString())
    {
        
    }
}