﻿using Mc2.CrudTest.SharedKernel;

namespace Mc2.CrudTest.Core.Customer.DomainEvents;

public class CustomerUpdated : DomainEvent
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }

    public CustomerUpdated(string aggregateId) : base(aggregateId)
    {
    }
}