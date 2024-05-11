using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Orders.DeleteOrderDetailFromCart;
using Restaurant.Application.Orders.GetOrderDetailsFromCart;
using Restaurant.Application.Payment.Buy;
using Restaurant.Controllers.Order.Response;
using Restaurant.Domain.Users.Enums;
using Restaurant.Infrastructure.Auth.Authorization.Attributes;
using Restaurant.Infrastructure.Auth.Authorization.CustomPolicy;

namespace Restaurant.Controllers.Cart;

[Route("api/v1.0/cart")]
public class CartController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CartController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get order details(products) in cart. If order isn't exist to create them.
    /// </summary>
    /// <returns>Order detail info.</returns>
    /// <response code="200">Returns order details info.</response>
    /// <response code="404">Order not found, because not created through database failure.</response> 
    /// <response code="500">Problem with database.</response>
    [HttpGet("see")]
    [HasPermission(Permissions.Cart)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderDetailResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> See()
    {
        var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        var query = new GetOrderDetailsFromCart(userId);
        var getOrderDetailsResult = await _mediator.Send(query);

        if (getOrderDetailsResult.IsError)
        {
            return Problem(getOrderDetailsResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<OrderDetailResponse>>(getOrderDetailsResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Delete product at cart in Restaurant
    /// </summary>
    /// <response code="204">Successful delete.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">Order with your user id is not found.</response>
    /// <response code="404">Product with that alias is not found.</response>
    /// <response code="500">Problem with database.</response>
    [HttpDelete("{alias}/cancel")]
    [HasPermission(Permissions.Cart)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    public async Task<IActionResult> Cancel(string alias)
    {
        var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        var command = new DeleteOrderDetailFromCart(alias, userId);
        var deleteOrderDetailResult = await _mediator.Send(command);

        if (deleteOrderDetailResult.IsError)
        {
            return Problem(deleteOrderDetailResult.Errors);
        }

        return NoContent();
    }

    [HttpPost("buy")]
    [HasPermission(Permissions.BuyOrder)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Buy()
    {
        var userId = new Guid(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);

        var buyCommand = new BuyCommand(userId);
        var buyResult = await _mediator.Send(buyCommand);

        if (buyResult.IsError)
        {
            return Problem(buyResult.Errors);
        }

        var response = _mapper.Map<OrderResponse>(buyResult.Value);
        return Ok(response);
    }
}