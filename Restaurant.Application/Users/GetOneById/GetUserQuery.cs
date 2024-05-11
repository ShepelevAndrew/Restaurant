using ErrorOr;
using MediatR;
using Restaurant.Domain.Users;

namespace Restaurant.Application.Users.GetOneById;

public record GetUserByIdQuery(
    Guid UserId) : IRequest<ErrorOr<User>>;