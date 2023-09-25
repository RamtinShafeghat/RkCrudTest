namespace Mc2.CrudTest.SharedKernel;

public abstract class Entity<TIdentity>
{
    public virtual TIdentity Id { get; protected set; }

    public override bool Equals(object obj)
    {
        if (obj is not Entity<TIdentity> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetRealType() != other.GetRealType())
            return false;

        return EqualityComparer<TIdentity>.Default.Equals(Id, ((Entity<TIdentity>)obj).Id);
    }
    public static bool operator ==(Entity<TIdentity> a, Entity<TIdentity> b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }
    public static bool operator !=(Entity<TIdentity> a, Entity<TIdentity> b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetRealType().ToString() + Id).GetHashCode();
    }
    private Type GetRealType()
    {
        const string EFCoreProxyPrefix = "Castle.Proxies.";

        var type = this.GetType();
        var typeString = type.ToString();

        if (typeString.Contains(EFCoreProxyPrefix))
            return type.BaseType!;

        return type;
    }
}
