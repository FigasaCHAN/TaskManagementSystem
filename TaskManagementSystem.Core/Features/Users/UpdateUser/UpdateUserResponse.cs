using TaskManagementSystem.Core.Dtos.User;

namespace TaskManagementSystem.Core.Features.Users.UpdateUser;

public record UpdateUserResponse
{
    public UserDto User { get; init; } = new();
}