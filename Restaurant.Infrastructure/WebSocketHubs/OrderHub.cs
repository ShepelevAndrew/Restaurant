using Microsoft.AspNetCore.SignalR;
using Restaurant.Domain.Users;
using Restaurant.Domain.Users.Entities;
using Restaurant.Domain.Users.Repositories;

namespace Restaurant.Infrastructure.WebSocketHubs;

public class OrderHub : Hub<IOrderHub>
{
    private readonly IUserRepository _userRepository;

    public OrderHub(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var user = await GetUserOrNull();
        if (user is null)
        {
            return;
        }

        if (user.RoleId == Role.Courier.Id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Role.Courier.Name); 
        }
        else if (user.RoleId == Role.Operator.Id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, Role.Operator.Name);
        }

        await base.OnConnectedAsync();
    }

    private async Task<User?> GetUserOrNull()
    {
        var userId = Context.UserIdentifier;
        if (!Guid.TryParse(userId, out var parsedUserId))
        {
            return null;
        }

        var user = await _userRepository.GetById(parsedUserId);
        return user;
    }
}