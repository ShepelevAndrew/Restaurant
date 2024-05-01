using Mapster;
using Restaurant.Controllers.User.Response;
using Restaurant.Domain.Users;

namespace Restaurant.Common.Mapping;

public class UsersMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Id, src => src.UserId);
    }
}