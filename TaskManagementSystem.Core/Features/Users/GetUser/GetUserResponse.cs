using TaskManagementSystem.Core.Dtos.User;

namespace TaskManagementSystem.Core.Features.Users.GetUser;

public record GetUserResponse
{
    public UserDto User { get; init; } = new();
}