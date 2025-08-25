using TaskManagementSystem.Core.Common;

namespace TaskManagementSystem.Core.Domain.ValueObjects;

public class Username : ValueObject
{
    private Username(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{nameof(Username)} cannot be null or empty.", nameof(value));

        if (value.Length > 50)
            throw new ArgumentException($"{nameof(Username)} cannot exceed 50 characters.", nameof(value));
    }

    public static Username Create(string value)
    {
        return new Username(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}