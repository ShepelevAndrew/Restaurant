using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Controllers.Permission.Response;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Controllers.Permission;

[Route("api/v1.0/permissions")]
public class PermissionController : ApiController
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public PermissionController(
        IPermissionRepository permissionRepository,
        IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var permissions = await _permissionRepository.Get();
        var response = _mapper.Map<IEnumerable<PermissionResponse>>(permissions);

        return Ok(response);
    }
}