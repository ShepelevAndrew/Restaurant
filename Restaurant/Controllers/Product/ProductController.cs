using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Common.Abstractions.BlobService;
using Restaurant.Application.Products.AddRating;
using Restaurant.Application.Products.AddToCart;
using Restaurant.Application.Products.Create;
using Restaurant.Application.Products.Delete;
using Restaurant.Application.Products.Get;
using Restaurant.Application.Products.GetById;
using Restaurant.Application.Products.GetOne;
using Restaurant.Application.Products.Update;
using Restaurant.Controllers.Order.Response;
using Restaurant.Controllers.Product.Request;
using Restaurant.Controllers.Product.Response;
using Restaurant.Domain.Users.Enums;
using Restaurant.Infrastructure.Auth.Authorization.Attributes;
using Restaurant.Infrastructure.Auth.Authorization.CustomPolicy;

namespace Restaurant.Controllers.Product;

[Route("api/v1.0/products")]
public class ProductController : ApiController
{
    private readonly IBlobService _blobService;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IBlobService blobService, IMediator mediator, IMapper mapper)
    {
        _blobService = blobService;
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Create product into restaurant
    /// </summary>
    /// <returns>Product info.</returns>
    /// <response code="200">Successful create. Returns product info.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="409">Problem with product information.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPost]
    [HasPermission(Permissions.Create)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(ProductRequest request)
    {
        var command = _mapper.Map<CreateProductCommand>(request);
        var createResult = await _mediator.Send(command);

        if (createResult.IsError)
        {
            return Problem(createResult.Errors);
        }

        var response = _mapper.Map<ProductResponse>(createResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Get all products in Restaurant
    /// </summary>
    /// <returns>Products</returns>
    /// <response code="200">Returns products info.</response>
    [HttpGet]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetProductsQuery();
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<ProductResponse>>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Get product in Restaurant
    /// </summary>
    /// <returns>User</returns>
    /// <response code="200">Returns product info.</response>
    /// <response code="404">Product with that alias is not found.</response>
    [AllowAnonymous]
    [HttpGet("{alias}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAlias(string alias)
    {
        var query = new GetProductQuery(alias);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<ProductResponse>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Get product in Restaurant
    /// </summary>
    /// <returns>User</returns>
    /// <response code="200">Returns product info.</response>
    /// <response code="404">Product with that id is not found.</response>
    [AllowAnonymous]
    [HttpGet("{id}/id")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetProductByIdQuery(id);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<ProductResponse>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Update product in Restaurant
    /// </summary>
    /// <returns>Updated product</returns>
    /// <response code="200">Successful update.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">Product with that alias is not found.</response>
    /// <response code="409">Problem with product information.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPut("{alias}")]
    [HasPermission(Permissions.Update)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(string alias, ProductRequest request)
    {
        var command = new UpdateProductCommand(alias, request.Name, request.Price, request.Weight, request.Description, request.CategoryId);
        var updateResult = await _mediator.Send(command);

        if (updateResult.IsError)
        {
            return Problem(updateResult.Errors);
        }

        var response = _mapper.Map<ProductResponse>(updateResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Add rating to product into restaurant
    /// </summary>
    /// <returns>Updated product info.</returns>
    /// <response code="200">Successful update. Returns product info.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPatch("add-rating/{alias}")]
    [HasPermission(Permissions.Read)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddMark(string alias, int mark)
    {
        var command = new AddRatingCommand(alias, mark);
        var addRatingResult = await _mediator.Send(command);

        if (addRatingResult.IsError)
        {
            return Problem(addRatingResult.Errors);
        }

        var response = _mapper.Map<ProductResponse>(addRatingResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Delete product in Restaurant
    /// </summary>
    /// <response code="204">Successful delete.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">Product with that alias is not found.</response>
    /// <response code="500">Problem with database.</response>
    [HttpDelete("{alias}")]
    [HasPermission(Permissions.Delete)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string alias)
    {
        var command = new DeleteProductCommand(alias);
        var deleteResult = await _mediator.Send(command);

        if (deleteResult.IsError)
        {
            return Problem(deleteResult.Errors);
        }

        return NoContent();
    }

    /// <summary>
    /// Add product to cart in Restaurant
    /// </summary>
    /// <returns>Order info</returns>
    /// <response code="200">Successful added.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">Product with that alias is not found.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPost("{alias}/add-to-card")]
    [HasPermission(Permissions.Cart)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    public async Task<IActionResult> AddToCart(string alias, int quantity)
    {
        var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        var command = new AddToCartCommand(alias, quantity, userId);
        var addToCartResult = await _mediator.Send(command);

        if (addToCartResult.IsError)
        {
            return Problem(addToCartResult.Errors);
        }

        var response = _mapper.Map<OrderResponse>(addToCartResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Create image for product at restaurant
    /// </summary>
    [HttpPost("{alias}/image")]
    [HasPermission(Permissions.Create)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    public IActionResult CreateImage(IFormFile image, string alias)
    {
        _blobService.Create(image, alias);
        return Ok();
    }

    /// <summary>
    /// Get image by product alias at restaurant
    /// </summary>
    [HttpGet("{alias}/image")]
    [AllowAnonymous]
    public IActionResult GetImage(string alias)
    {
        var file = _blobService.GetByAlias(alias);
        if (file is null)
        {
            return NotFound();
        }

        return File(file.Content.Stream, file.ContentType, file.Name);
    }

    [HttpPut("{alias}/image")]
    [HasPermission(Permissions.Update)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    public IActionResult UpdateImage(IFormFile file, string alias)
    {
        _blobService.Update(file, alias);
        return Ok();
    }
}