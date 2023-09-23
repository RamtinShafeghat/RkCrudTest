﻿namespace Mc2.Crud.SharedKernel.Contracts;

public interface IAggregateRoot
{
    int Version { get; }

    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
}
