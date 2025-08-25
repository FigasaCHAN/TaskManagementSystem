using System.Net.Mail;
using TaskManagementSystem.Core.Common;

namespace TaskManagementSystem.Core.Domain.ValueObjects.Common;

public class Email : ValueObject
{
    private Email(string value)
    {
        Validate(value);
        Value = value;
    }

    public string Value { get; }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty.", nameof(value));
        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email format.", nameof(value));
    }

    public static Email Create(string value)
    {
        return new Email(value);
    }

    private static bool IsValidEmail(string value)
    {
        try
        {
            var addr = new MailAddress(value);
            return addr.Address == value;
        }
        catch
        {
            return false;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}