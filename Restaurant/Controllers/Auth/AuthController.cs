using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Auth.Login;
using Restaurant.Application.Auth.Register;
using Restaurant.Application.Auth.VerificationAccount;
using Restaurant.Controllers.Auth.Common;
using Restaurant.Controllers.Auth.Login;
using Restaurant.Controllers.Auth.Register;

namespace Restaurant.Controllers.Auth;

[Route("api/v1.0/auth")]
public class AuthController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Login into restaurant
    /// </summary>
    /// <returns>User info and bearer token.</returns>
    /// <response code="200">Successful login. Returns user info and bearer token.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="409">Problem with authentication information.</response>
    [HttpPost("login")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var loginResult = await _mediator.Send(query);

        if (loginResult.IsError)
        {
            return Problem(loginResult.Errors);
        }

        var response = _mapper.Map<AuthenticationResponse>(loginResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Register into restaurant
    /// </summary>
    /// <returns>User info and bearer token.</returns>
    /// <response code="200">Successful register. Returns user info and bearer token.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="409">Problem with authentication information.</response>
    [HttpPost("register")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var registerResult = await _mediator.Send(command);

        if (registerResult.IsError)
        {
            return Problem(registerResult.Errors);
        }

        var response = _mapper.Map<AuthenticationResponse>(registerResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Confirm your email address
    /// </summary>
    /// <returns>User info and bearer token.</returns>
    /// <response code="200">Successful verified account. Returns user info and bearer token.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="404">User with that email address is not found.</response>
    /// <response code="409">Problem with authentication information.</response>
    [HttpGet("confirm-email")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmEmail(string email, string code)
    {
        var command = new VerificationAccountCommand(email, code);
        var verificationResult = await _mediator.Send(command);

        if (verificationResult.IsError)
        {
            return Problem(verificationResult.Errors);
        }

        var response = _mapper.Map<AuthenticationResponse>(verificationResult.Value);
        return Ok(response);
    }
}