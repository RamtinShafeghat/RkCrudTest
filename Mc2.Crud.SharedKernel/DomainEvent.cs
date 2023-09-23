using Mc2.Crud.SharedKernel.Contracts;
using System.Text.Json.Serialization;

namespace Mc2.CrudTest.SharedKernel;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; }

    public string AggregateId { get; }

    [JsonIgnore]
    public int Version { get; private set; }

    [JsonIgnore]
    public int Sequence { get; private set; }

    public DateTime CreatedAt { get; set; }

    protected DomainEvent(string aggregateId)
    {
        this.Id = Guid.NewGuid();
        this.AggregateId = aggregateId;
        this.Version = 0;
        this.Sequence = 0;
        this.CreatedAt = DateTime.Now;
    }

    [JsonConstructor]
    protected DomainEvent(Guid id, string aggregateId, DateTime createdAt)
    {
        this.Id = id;
        this.AggregateId = aggregateId;
        this.CreatedAt = createdAt;
    }

    public void WithVersionAndSequence(int version, int sequence)
    {
        this.Version = version;
        this.Sequence = sequence;
    }
}