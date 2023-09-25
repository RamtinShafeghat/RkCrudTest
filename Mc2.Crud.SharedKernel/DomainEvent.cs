using Mc2.CrudTest.SharedKernel.Contracts;

namespace Mc2.CrudTest.SharedKernel;

public abstract class DomainEvent : IDomainEvent
{
    protected DomainEvent(string aggregateId)
    {
        this.Id = Guid.NewGuid();
        this.AggregateId = aggregateId;
        this.CreatedAt = DateTime.Now;
    }

    public Guid Id { get; }

    public string AggregateId { get; }

    public DateTime CreatedAt { get; }
}