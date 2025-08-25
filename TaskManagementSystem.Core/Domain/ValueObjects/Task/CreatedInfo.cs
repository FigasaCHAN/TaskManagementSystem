using TaskManagementSystem.Core.Common;

namespace TaskManagementSystem.Core.Domain.ValueObjects.Task;

public class CreatedInfo : ValueObject
{
    private CreatedInfo(int createdBy)
    {
        Validate(createdBy);

        CreatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public DateTime CreatedAt { get; }
    public int CreatedBy { get; }

    private static void Validate(int createdBy)
    {
        if (createdBy <= 0) throw new ArgumentException("CreatedBy must be a positive integer.", nameof(createdBy));
    }

    public static CreatedInfo Create(int createdBy)
    {
        return new CreatedInfo(createdBy);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CreatedAt;
        yield return CreatedBy;
    }
}