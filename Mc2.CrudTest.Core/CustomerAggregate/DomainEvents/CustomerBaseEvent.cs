﻿namespace Mc2.CrudTest.Core.CustomerAggregate.DomainEvents;

public class CustomerBaseEvent : DomainEvent
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }

    public CustomerBaseEvent(string aggregateId) : base(aggregateId)
    {
    }
}
