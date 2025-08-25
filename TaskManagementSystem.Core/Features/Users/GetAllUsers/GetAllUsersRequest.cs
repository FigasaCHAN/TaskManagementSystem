using MediatR;

namespace TaskManagementSystem.Core.Features.Users.GetAllUsers;

public record GetAllUsersRequest : IRequest<GetAllUsersResponse>
{
    public int? Page { get; init; }
    public int? PageSize { get; init; }
}