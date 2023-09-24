using Mc2.CrudTest.SharedKernel;

namespace Mc2.CrudTest.Core.CustomerAggregate.DomainEvents;

public class CustomerCreated : DomainEvent
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }

    public CustomerCreated(string aggregateId) : base(aggregateId)
    {
    }
}
