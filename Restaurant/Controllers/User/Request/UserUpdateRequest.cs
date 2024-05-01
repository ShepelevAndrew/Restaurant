namespace Restaurant.Controllers.User.Request;

public record UserUpdateRequest(
    string? Firstname,
    string? Lastname,
    string? Phone);