namespace Restaurant.Controllers.Auth.Common;

public record UserResponse(
    string Firstname,
    string Lastname,
    string Email,
    string Phone);