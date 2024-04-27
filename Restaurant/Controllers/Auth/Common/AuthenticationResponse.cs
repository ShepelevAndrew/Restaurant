namespace Restaurant.Controllers.Auth.Common;

public record AuthenticationResponse(
    UserResponse User,
    string Token);