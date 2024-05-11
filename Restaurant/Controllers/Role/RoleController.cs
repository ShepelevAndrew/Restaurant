using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Roles.Create;
using Restaurant.Application.Roles.Get;
using Restaurant.Application.Roles.GetOneById;
using Restaurant.Controllers.Role.Request;
using Restaurant.Controllers.Role.Response;
using Restaurant.Domain.Users.Enums;
using Restaurant.Infrastructure.Auth.Authorization.Attributes;
using Restaurant.Infrastructure.Auth.Authorization.CustomPolicy;

namespace Restaurant.Controllers.Role;

[Route("api/v1.0/roles")]
public class RoleController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RoleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(AuthPolicy.VerifiedAccount)]
    public async Task<IActionResult> Get()
    {
        var query = new GetRolesQuery();
        var getRolesResult = await _mediator.Send(query);

        if (getRolesResult.IsError)
        {
            return Problem(getRolesResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<RoleResponse>>(getRolesResult.Value);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [Authorize(AuthPolicy.VerifiedAccount)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetRoleByIdQuery(id);
        var getRoleResult = await _mediator.Send(query);

        if (getRoleResult.IsError)
        {
            return Problem(getRoleResult.Errors);
        }

        var response = _mapper.Map<RoleResponse>(getRoleResult.Value);
        return Ok(response);
    }

    [HttpPost]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [HasPermission(Permissions.CreateRole)]
    public async Task<IActionResult> Create(RoleRequest request)
    {
        var command = _mapper.Map<CreateRoleCommand>(request);
        var createResult = await _mediator.Send(command);

        if (createResult.IsError)
        {
            return Problem(createResult.Errors);
        }

        var response = _mapper.Map<RoleResponse>(createResult.Value);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}