using Mc2.CrudTest.SharedKernel.Contracts;

namespace Mc2.CrudTest.SharedKernel;

public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>, IAggregateRoot
{
    private readonly List<IDomainEvent> domainEvents = new();

    protected AggregateRoot()
    {
    }
    protected AggregateRoot(IEnumerable<IDomainEvent> events)
    {
        if (events == null)
        {
            return;
        }

        foreach (IDomainEvent @event in events)
        {
            Mutate(@event);
            Version++;
        }
    }

    public int Version { get; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent @event)
    {
        domainEvents.Add(@event);
    }
    protected void RemoveDomainEvent(IDomainEvent @event)
    {
        domainEvents.Remove(@event);
    }
    protected void ClearDomainEvents()
    {
        domainEvents.Clear();
    }

    protected void Apply(IEnumerable<IDomainEvent> events)
    {
        foreach (IDomainEvent @event in events)
            Apply(@event);
    }
    protected void Apply(IDomainEvent @event)
    {
        Mutate(@event);
        AddDomainEvent(@event);
    }

    private void Mutate(IDomainEvent @event)
    {
        ((dynamic)this).On((dynamic)@event);
    }
}
