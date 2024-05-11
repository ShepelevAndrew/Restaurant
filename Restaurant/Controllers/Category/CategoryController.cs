using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Categories.Create;
using Restaurant.Application.Categories.Delete;
using Restaurant.Application.Categories.Get;
using Restaurant.Application.Categories.GetOne;
using Restaurant.Application.Categories.Update;
using Restaurant.Controllers.Category.Request;
using Restaurant.Controllers.Category.Response;
using Restaurant.Domain.Users.Enums;
using Restaurant.Infrastructure.Auth.Authorization.Attributes;
using Restaurant.Infrastructure.Auth.Authorization.CustomPolicy;

namespace Restaurant.Controllers.Category;

[Route("api/v1.0/category")]
public class CategoryController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Create category into restaurant
    /// </summary>
    /// <returns>Category info.</returns>
    /// <response code="200">Successful create. Returns category info.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="409">Problem with category information.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPost]
    [HasPermission(Permissions.Create)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CategoryRequest request)
    {
        var command = _mapper.Map<CreateCategoryCommand>(request);
        var createResult = await _mediator.Send(command);

        if (createResult.IsError)
        {
            return Problem(createResult.Errors);
        }

        var response = _mapper.Map<CategoryResponse>(createResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Get all categories in Restaurant
    /// </summary>
    /// <returns>Categories</returns>
    /// <response code="200">Returns categories info.</response>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetCategoriesQuery();
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<GetCategoryResponse>>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Get category in Restaurant
    /// </summary>
    /// <returns>Category</returns>
    /// <response code="200">Returns category info.</response>
    /// <response code="404">Category with that alias is not found.</response>
    [AllowAnonymous]
    [HttpGet("{alias}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAlias(string alias)
    {
        var query = new GetCategoryQuery(alias);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<GetCategoryResponse>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Update category in Restaurant
    /// </summary>
    /// <returns>Updated category</returns>
    /// <response code="200">Successful update.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">Category with that alias is not found.</response>
    /// <response code="409">Problem with category information.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPut("{alias}")]
    [HasPermission(Permissions.Update)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(string alias, CategoryRequest request)
    {
        var command = new UpdateCategoryCommand(alias, request.Name, request.Description, request.ParentId);
        var updateResult = await _mediator.Send(command);
        if (updateResult.IsError)
        {
            return Problem(updateResult.Errors);
        }

        var response = _mapper.Map<CategoryResponse>(updateResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Delete category in Restaurant
    /// </summary>
    /// <response code="204">Successful delete.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">Category with that alias is not found.</response>
    /// <response code="500">Problem with database.</response>
    [HttpDelete("{alias}")]
    [HasPermission(Permissions.Delete)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string alias)
    {
        var command = new DeleteCategoryCommand(alias);
        var deleteResult = await _mediator.Send(command);

        if (deleteResult.IsError)
        {
            return Problem(deleteResult.Errors);
        }

        return NoContent();
    }
}