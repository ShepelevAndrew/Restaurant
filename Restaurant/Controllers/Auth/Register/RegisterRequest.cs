namespace Restaurant.Controllers.Auth.Register;

public record RegisterRequest(
    string Firstname,
    string Lastname,
    string Email,
    string Phone,
    string Password);