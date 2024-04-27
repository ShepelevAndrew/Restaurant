namespace Restaurant.Controllers.Auth.Login;

public record LoginRequest(
    string Email,
    string Password);