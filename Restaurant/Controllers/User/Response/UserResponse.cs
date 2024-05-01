namespace Restaurant.Controllers.User.Response;

public record UserResponse(
    Guid Id,
    string Firstname,
    string Lastname,
    string Email,
    string Phone,
    string IsEmailConfirmed);