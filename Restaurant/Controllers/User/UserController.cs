﻿using System.Security.Claims;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Users.ChangeUserRole;
using Restaurant.Application.Users.Delete;
using Restaurant.Application.Users.Get;
using Restaurant.Application.Users.GetOne;
using Restaurant.Application.Users.GetOneById;
using Restaurant.Application.Users.Update;
using Restaurant.Application.Users.UpdateById;
using Restaurant.Controllers.User.Request;
using Restaurant.Controllers.User.Response;
using Restaurant.Domain.Users.Enums;
using Restaurant.Infrastructure.Auth.Authorization.Attributes;
using Restaurant.Infrastructure.Auth.Authorization.CustomPolicy;

namespace Restaurant.Controllers.User;

[Route("api/v1.0/users")]
public class UserController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get your profile in Restaurant
    /// </summary>
    /// <returns>User</returns>
    /// <response code="200">Returns user info.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">User with that email address is not found.</response>
    [HttpGet("my")]
    [HasPermission(Permissions.Read)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyProfile()
    {
        var userEmail = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
        if (userEmail is null)
        {
            return BadRequest();
        }

        var query = new GetUserQuery(userEmail);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<UserResponse>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Get all users in Restaurant
    /// </summary>
    /// <returns>Users</returns>
    /// <response code="200">Returns users info.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    [HttpGet]
    [HasPermission(Permissions.ReadUsers)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetUsersQuery();
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<IEnumerable<UserResponse>>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Get user in Restaurant
    /// </summary>
    /// <returns>User</returns>
    /// <response code="200">Returns user info.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">User with that email address is not found.</response>
    [HttpGet("{email}")]
    [HasPermission(Permissions.Read)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var query = new GetUserQuery(email);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<UserResponse>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Get user in Restaurant
    /// </summary>
    /// <returns>User</returns>
    /// <response code="200">Returns user info.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">User with that user id is not found.</response>
    [HttpGet("{userId}/id")]
    [HasPermission(Permissions.Read)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid userId)
    {
        var query = new GetUserByIdQuery(userId);
        var getResult = await _mediator.Send(query);

        if (getResult.IsError)
        {
            return Problem(getResult.Errors);
        }

        var response = _mapper.Map<UserResponse>(getResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Update user in Restaurant
    /// </summary>
    /// <returns>Updated user</returns>
    /// <response code="200">Successful update.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">User with that email address is not found.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPatch("{email}")]
    [HasPermission(Permissions.UpdateUsers)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(string email, UserUpdateRequest updateRequest)
    {
        var command = new UpdateUserCommand(updateRequest.Firstname, updateRequest.Lastname, email, updateRequest.Phone);
        var updateResult = await _mediator.Send(command);
        if (updateResult.IsError)
        {
            return Problem(updateResult.Errors);
        }

        var response = _mapper.Map<UserResponse>(updateResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Update user in Restaurant
    /// </summary>
    /// <returns>Updated user</returns>
    /// <response code="200">Successful update.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">User with that email address is not found.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPatch("my")]
    [HasPermission(Permissions.UpdateMyUser)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMy(UserUpdateRequest updateRequest)
    {
        var userId = new Guid(HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

        var command = new UpdateUserByIdCommand(userId, updateRequest.Firstname, updateRequest.Lastname, updateRequest.Phone);
        var updateResult = await _mediator.Send(command);
        if (updateResult.IsError)
        {
            return Problem(updateResult.Errors);
        }

        var response = _mapper.Map<UserResponse>(updateResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Update user in Restaurant
    /// </summary>
    /// <returns>Updated user</returns>
    /// <response code="200">Successful update.</response>
    /// <response code="400">Incorrect validation request.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account don't have access.</response>
    /// <response code="404">User with that email address is not found.</response>
    /// <response code="404">Role with that id is not found.</response>
    /// <response code="500">Problem with database.</response>
    [HttpPatch("{email}/change-role")]
    [HasPermission(Permissions.UpdateUsers)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeRole(string email, int roleId)
    {
        var command = new ChangeUserRoleCommand(email, roleId);
        var changeResult = await _mediator.Send(command);
        if (changeResult.IsError)
        {
            return Problem(changeResult.Errors);
        }

        var response = _mapper.Map<UserResponse>(changeResult.Value);
        return Ok(response);
    }

    /// <summary>
    /// Delete user in Restaurant
    /// </summary>
    /// <response code="204">Successful delete.</response>
    /// <response code="401">You are not authenticate.</response>
    /// <response code="403">Your account is not verified.</response>
    /// <response code="404">User with that email address is not found.</response>
    /// <response code="500">Problem with database.</response>
    [HttpDelete("{email}")]
    [HasPermission(Permissions.DeleteUsers)]
    [Authorize(AuthPolicy.VerifiedAccount)]
    [Produces("application/json")]
    public async Task<IActionResult> Delete(string email)
    {
        var command = new DeleteUserCommand(email);
        var deleteResult = await _mediator.Send(command);

        if (deleteResult.IsError)
        {
            return Problem(deleteResult.Errors);
        }

        return NoContent();
    }
}