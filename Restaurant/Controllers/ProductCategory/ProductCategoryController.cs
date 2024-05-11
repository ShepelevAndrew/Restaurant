using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.ProductsCategory.GetProducts;
using Restaurant.Controllers.Product.Response;

namespace Restaurant.Controllers.ProductCategory;

[Route("api/v1.0/product-category")]
public class ProductCategoryController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductCategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get product in Restaurant
    /// </summary>
    /// <returns>User</returns>
    /// <response code="200">Returns product info.</response>
    /// <response code="404">Product with that id is not found.</response>
    [AllowAnonymous]
    [HttpGet("{alias}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsByCategory(string alias)
    {
        var query = new GetProductsByCategory(alias);
        var getResult = await _mediator.Send(query);
        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<ProductResponse>>(getResult.Value);
        return Ok(response);
    }
}