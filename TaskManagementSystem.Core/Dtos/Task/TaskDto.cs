namespace TaskManagementSystem.Core.Dtos.Task;

public record TaskDto
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public int AssignedUserId { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public int? LastModifiedBy { get; init; }
    public DateTime? LastModifiedAt { get; init; }
    public string Status { get; init; }
    public bool Deleted { get; init; }
}