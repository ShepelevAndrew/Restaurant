using ErrorOr;
using MediatR;
using Restaurant.Domain.Users.Entities;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Application.Roles.Get;

public class GetRolesQueryHandler
    : IRequestHandler<GetRolesQuery, ErrorOr<List<Role>>>
{
    private readonly IRoleRepository _roleRepository;

    public GetRolesQueryHandler(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<ErrorOr<List<Role>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var products = await _roleRepository.Get();
        return products.ToList();
    }
}