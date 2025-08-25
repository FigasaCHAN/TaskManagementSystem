using TaskManagementSystem.Core.Dtos.User;

namespace TaskManagementSystem.Core.Features.Users.CreateUser;

public record CreateUserResponse
{
    public UserDto User { get; init; } = new();
}