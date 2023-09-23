using Mc2.CrudTest.SharedKernel;

namespace Mc2.CrudTest.Core.Customer.DomainEvents;

public class CustomerDeleted : DomainEvent
{
    public bool IsDeleted { get; set; }

    public CustomerDeleted(string aggregateId) : base(aggregateId)
    {
    }
}
