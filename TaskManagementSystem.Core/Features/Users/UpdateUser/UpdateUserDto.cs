using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.Features.Users.UpdateUser;

public record UpdateUserDto
{
    [StringLength(50, MinimumLength = 3)] public string? Username { get; init; }

    [EmailAddress] public string? Email { get; init; }

    [MinLength(8)] public string? Password { get; init; }
}