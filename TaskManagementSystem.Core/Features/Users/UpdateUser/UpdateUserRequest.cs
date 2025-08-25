using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Users.UpdateUser;

public record UpdateUserRequest : IRequest<UpdateUserResponse>
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Id must be a positive integer.")]
    public int Id { get; init; }

    [Required] public UpdateUserDto UpdateUserDto { get; init; } = new();

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "ModifiedBy must be a positive integer.")]
    public int ModifiedBy { get; init; }
}