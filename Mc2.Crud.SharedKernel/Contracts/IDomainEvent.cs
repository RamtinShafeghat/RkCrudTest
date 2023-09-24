namespace Mc2.Crud.SharedKernel.Contracts;

public interface IDomainEvent 
{
    DateTime CreatedAt { get; }
}
