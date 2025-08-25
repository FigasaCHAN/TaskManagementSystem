using TaskManagementSystem.Core.Common;

namespace TaskManagementSystem.Core.Domain.ValueObjects.Task;

public class LastModifiedInfo : ValueObject
{
    private LastModifiedInfo()
    {
    }

    private LastModifiedInfo(int lastModifiedBy)
    {
        Validate(lastModifiedBy);

        LastModifiedBy = lastModifiedBy;
        LastModifiedAt = DateTime.UtcNow;
    }

    public DateTime? LastModifiedAt { get; }
    public int? LastModifiedBy { get; }

    private static void Validate(int lastModifiedBy)
    {
        if (lastModifiedBy <= 0)
            throw new ArgumentException("LastModifiedBy must be a positive integer.", nameof(lastModifiedBy));
    }

    public static LastModifiedInfo Create(int lastModifiedBy)
    {
        return new LastModifiedInfo(lastModifiedBy);
    }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return LastModifiedAt;
        yield return LastModifiedBy;
    }
}