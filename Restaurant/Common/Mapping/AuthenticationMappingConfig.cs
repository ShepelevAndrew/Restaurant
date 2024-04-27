using Mapster;
using Restaurant.Application.Auth.Common;
using Restaurant.Application.Auth.Login;
using Restaurant.Application.Auth.Register;
using Restaurant.Controllers.Auth.Common;
using Restaurant.Controllers.Auth.Login;
using Restaurant.Controllers.Auth.Register;

namespace Restaurant.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.User, src => src.User)
            .Map(dest => dest.Token, src => src.Token);
    }
}