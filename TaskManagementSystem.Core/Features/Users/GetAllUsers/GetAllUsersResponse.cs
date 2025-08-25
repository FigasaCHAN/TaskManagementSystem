using TaskManagementSystem.Core.Dtos.User;

namespace TaskManagementSystem.Core.Features.Users.GetAllUsers;

public record GetAllUsersResponse
{
    public IEnumerable<UserDto> Users { get; init; } = [];
    public int TotalCount { get; init; }
}