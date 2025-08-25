namespace TaskManagementSystem.Core.Common;

public abstract class Entity<TIdentity> : IEquatable<Entity<TIdentity>>
{
    public TIdentity Id { get; protected set; } = default!;

    #region Overrides of Object

    public override string ToString()
    {
        return $"{GetType().Name}#[Identity={Id}]";
    }

    #endregion Overrides of Object

    public static bool operator ==(Entity<TIdentity>? a, Entity<TIdentity>? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<TIdentity>? a, Entity<TIdentity>? b)
    {
        return !(a == b);
    }

    #region Constructors

    protected Entity(TIdentity id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    #endregion Constructors

    #region Implementation of IEquatable<Entity>

    public bool Equals(Entity<TIdentity>? other)
    {
        if (ReferenceEquals(null, other)) return false;

        if (ReferenceEquals(this, other)) return true;

        return Equals(Id, other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;

        if (ReferenceEquals(this, obj)) return true;

        return obj.GetType() == GetType() && Equals((Entity<TIdentity>)obj);
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode() * 907 + Id!.GetHashCode();
    }

    #endregion Implementation of IEquatable<Entity>
}