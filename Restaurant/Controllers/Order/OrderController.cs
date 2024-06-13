using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Orders.Cancelled;
using Restaurant.Application.Orders.Cooked;
using Restaurant.Application.Orders.Get;
using Restaurant.Application.Orders.GetByOne;
using Restaurant.Application.Orders.Paid;
using Restaurant.Application.Orders.Shipped;
using Restaurant.Application.Orders.Verification;
using Restaurant.Controllers.Order.Request;
using Restaurant.Controllers.Order.Response;
using Restaurant.Domain.Orders.Enums;
using Restaurant.Domain.Users.Enums;
using Restaurant.Infrastructure.Auth.Authorization.Attributes;

namespace Restaurant.Controllers.Order;

[Route("api/v1.0/orders")]
public class OrderController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OrderController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("my")]
    [HasPermission(Permissions.Read)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = new Guid(HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

        var query = new GetUserOrdersQuery(userId);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<OrderResponse>>(getResult.Value);
        return Ok(response);
    }

    [HttpGet("bought")]
    [HasPermission(Permissions.GetBoughtOrders)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBoughtOrders()
    {
        var query = new GetOrdersQuery(OrderStatus.Checkout);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<OrderResponse>>(getResult.Value);
        return Ok(response);
    }

    [HttpGet("shipped")]
    [HasPermission(Permissions.GetBoughtOrders)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShippedOrders()
    {
        var query = new GetOrdersQuery(OrderStatus.Shipped);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<OrderResponse>>(getResult.Value);
        return Ok(response);
    }

    [HttpGet("confirm")]
    [HasPermission(Permissions.GetVerifiedOrders)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetConfirmOrders()
    {
        var query = new GetOrdersQuery(OrderStatus.Confirmed);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<OrderResponse>>(getResult.Value);
        return Ok(response);
    }

    [HttpGet("cooked")]
    [HasPermission(Permissions.GetVerifiedOrders)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCookedOrders()
    {
        var query = new GetOrdersQuery(OrderStatus.Cooked);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<OrderResponse>>(getResult.Value);
        return Ok(response);
    }

    [HttpGet("paid")]
    [HasPermission(Permissions.GetPaidOrders)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaidOrders()
    {
        var query = new GetOrdersQuery(OrderStatus.Paid);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<OrderResponse>>(getResult.Value);
        return Ok(response);
    }

    [HttpGet("cancelled")]
    [HasPermission(Permissions.GetCancelledOrders)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCancelledOrders()
    {
        var query = new GetOrdersQuery(OrderStatus.Cancelled);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<OrderResponse>>(getResult.Value);
        return Ok(response);
    }

    [HttpPatch("{orderId}/verified")]
    [HasPermission(Permissions.VerifyOrder)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> VerifyOrder(Guid orderId)
    {
        var command = new VerificationOrderCommand(orderId);
        var verificationResult = await _mediator.Send(command);

        if (verificationResult.IsError)
        {
            return Problem(verificationResult.Errors);
        }

        var response = _mapper.Map<OrderResponse>(verificationResult.Value);
        return Ok(response);
    }

    [HttpPatch("{orderId}/cooked")]
    [HasPermission(Permissions.VerifyOrder)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CookedOrder(Guid orderId)
    {
        var command = new CookedOrderCommand(orderId);
        var cookedResult = await _mediator.Send(command);

        if (cookedResult.IsError)
        {
            return Problem(cookedResult.Errors);
        }

        var response = _mapper.Map<OrderResponse>(cookedResult.Value);
        return Ok(response);
    }

    [HttpPatch("{orderId}/shipped")]
    [HasPermission(Permissions.ShippedOrder)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ShippedOrder(Guid orderId)
    {
        var command = new ShippedOrderCommand(orderId);
        var shippedResult = await _mediator.Send(command);

        if (shippedResult.IsError)
        {
            return Problem(shippedResult.Errors);
        }

        var response = _mapper.Map<OrderResponse>(shippedResult.Value);
        return Ok(response);
    }

    [HttpPatch("{orderId}/paid")]
    [HasPermission(Permissions.PaidOrder)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> PaidOrder(Guid orderId)
    {
        var command = new PaidOrderCommand(orderId);
        var paidResult = await _mediator.Send(command);

        if (paidResult.IsError)
        {
            return Problem(paidResult.Errors);
        }

        var response = _mapper.Map<OrderResponse>(paidResult.Value);
        return Ok(response);
    }

    [HttpPatch("{orderId}/cancelled")]
    [HasPermission(Permissions.CancelledOrder)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CancelledOrder(Guid orderId, CancelledOrderRequest request)
    {
        var command = new CancelledOrderCommand(orderId, request.Description);
        var cancelledResult = await _mediator.Send(command);

        if (cancelledResult.IsError)
        {
            return Problem(cancelledResult.Errors);
        }

        var response = _mapper.Map<OrderResponse>(cancelledResult.Value);
        return Ok(response);
    }
}