namespace Mc2.CrudTest.Core.CustomerAggregate.DomainEvents;

public class CustomerUpdated : CustomerBaseEvent
{
    public CustomerUpdated(string aggregateId) : base(aggregateId)
    {
    }
}
