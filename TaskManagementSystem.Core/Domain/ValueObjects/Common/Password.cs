using TaskManagementSystem.Core.Common;

namespace TaskManagementSystem.Core.Domain.ValueObjects.Common;

public class Password : ValueObject
{
    private Password(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be empty.", nameof(value));
    }

    public static Password Create(string value)
    {
        return new Password(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}