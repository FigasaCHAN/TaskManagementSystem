using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.Features.Tasks.UpdateTask;

public record UpdateTaskDto
{
    [StringLength(100, MinimumLength = 3)] public string? Title { get; init; }

    [StringLength(500, MinimumLength = 3)] public string? Description { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "AssignedUserId must be a positive integer.")]
    public int? AssignedUserId { get; init; }

    public string? Status { get; init; }
}