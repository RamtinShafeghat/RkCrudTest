namespace Mc2.CrudTest.SharedKernel.Contracts;

public interface IDomainEvent 
{
    DateTime CreatedAt { get; }
}
