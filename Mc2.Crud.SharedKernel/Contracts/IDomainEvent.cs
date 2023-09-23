namespace Mc2.Crud.SharedKernel.Contracts;

public interface IDomainEvent
{
    int Sequence { get; }

    int Version { get; }

    DateTime CreatedAt { get; set; }
}
