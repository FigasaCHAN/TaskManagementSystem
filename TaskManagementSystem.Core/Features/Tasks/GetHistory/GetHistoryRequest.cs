using System.ComponentModel.DataAnnotations;
using MediatR;

namespace TaskManagementSystem.Core.Features.Tasks.GetHistory;

public record GetHistoryRequest : IRequest<GetHistoryResponse>
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "TaskId must be a positive integer.")]
    public int TaskId { get; init; }

    public int? PageNumber { get; init; }
    public int? PageSize { get; init; }
}