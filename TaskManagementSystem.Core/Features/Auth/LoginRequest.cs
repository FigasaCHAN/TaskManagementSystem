using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Auth;

public class LoginRequest : IRequest<LoginResponse>

{
    [EmailAddress] public string Email { get; set; } = string.Empty;

    [MinLength(8)] public string Password { get; set; } = string.Empty;
}