namespace Mc2.CrudTest.Core.CustomerAggregate.DomainEvents;

public class CustomerCreated : CustomerBaseEvent
{
    public CustomerCreated(string aggregateId) : base(aggregateId)
    {
    }
}
