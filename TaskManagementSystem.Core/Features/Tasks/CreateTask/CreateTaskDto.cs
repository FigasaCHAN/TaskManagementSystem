using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Core.Features.Tasks.CreateTask;

public record CreateTaskDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; init; }

    [Required]
    [StringLength(500, MinimumLength = 3)]
    public string Description { get; init; }

    [Required] public int AssignedUserId { get; init; }
}