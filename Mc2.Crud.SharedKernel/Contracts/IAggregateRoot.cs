namespace Mc2.CrudTest.SharedKernel.Contracts;

public interface IAggregateRoot
{
    int Version { get; }

    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
}
