using ErrorOr;
using MediatR;
using Restaurant.Domain.Users;

namespace Restaurant.Application.Users.GetOne;

public record GetUserQuery(
    string Email) : IRequest<ErrorOr<User>>;