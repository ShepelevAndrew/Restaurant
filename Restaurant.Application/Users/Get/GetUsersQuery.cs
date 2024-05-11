using ErrorOr;
using MediatR;
using Restaurant.Domain.Users;

namespace Restaurant.Application.Users.Get;

public record GetUsersQuery : IRequest<ErrorOr<List<User>>>;