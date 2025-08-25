using TaskManagementSystem.Core.Common;
using TaskManagementSystem.Core.Domain.ValueObjects;
using TaskManagementSystem.Core.Domain.ValueObjects.Common;

namespace TaskManagementSystem.Core.Domain.AggregateRoots;

public class User : AggregateRoot<int>
{
    private User()
    {
    }

    public User(Username username, Email email, Password password)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username), $"{nameof(Username)} cannot be null.");
        Email = email ?? throw new ArgumentNullException(nameof(email), $"{nameof(Email)} cannot be null.");
        Password = password ?? throw new ArgumentNullException(nameof(password), $"{nameof(Email)} cannot be null.");
    }

    public Username Username { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public bool Deleted { get; private set; }
    public long Version { get; private set; }

    public void Delete()
    {
        Deleted = true;
    }

    public void Update(Username? username, Email? email, Password? password)
    {
        if (username != null) Username = username;
        if (email != null) Email = email;
        if (password != null) Password = password;
    }
}