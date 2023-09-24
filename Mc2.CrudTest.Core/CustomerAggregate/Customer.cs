using Mc2.Crud.SharedKernel;
using Mc2.Crud.SharedKernel.Contracts;
using Mc2.CrudTest.Core.CustomerAggregate.DomainEvents;

namespace Mc2.CrudTest.Core.CustomerAggregate;

public class Customer : AggregateRoot<Guid>
{
    private Customer() { }

    public Customer(IEnumerable<IDomainEvent> events) : base(events) { }

    public override Guid Id { get; protected set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; set; }
    public string BankAccountNumber { get; private set; }
    public bool IsDeleted { get; private set; }

    public static Customer CreateCustomer(
        string firstName,
        string lastName,
        DateOnly dateOfBirth, 
        string email, 
        string phoneNumber,
        string bankAccountNumber)
    {
        var customer = new Customer();
        customer.Apply(new CustomerCreated(Guid.NewGuid().ToString())
        {
            FirstName = firstName,
            LastName = lastName, 
            DateOfBirth = dateOfBirth, 
            Email = email, 
            PhoneNumber = phoneNumber,
            BankAccountNumber = bankAccountNumber
        });

        return customer;
    }

    public static void UpdateCustomer(
        Customer existingCustomer,
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        string email,
        string PhoneNumber,
        string bankAccountNumber)
    {
        existingCustomer.Apply(new CustomerUpdated(existingCustomer.Id.ToString())
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Email = email,
            PhoneNumber = PhoneNumber,
            BankAccountNumber = bankAccountNumber
        });
    }

    public static void DeleteCustomer(
        Customer existingCustomer)
    {
        existingCustomer.Apply(new CustomerDeleted(existingCustomer.Id.ToString())
        {
            IsDeleted = true
        });
    }

    public void On(CustomerCreated @event)
    {
        Id = Guid.Parse(@event.AggregateId);
        FirstName = @event.FirstName;
        LastName = @event.LastName;
        DateOfBirth = @event.DateOfBirth;
        Email = @event.Email;
        PhoneNumber = @event.PhoneNumber;
        BankAccountNumber = @event.BankAccountNumber;
    }
    public void On(CustomerUpdated @event)
    {
        Id = Guid.Parse(@event.AggregateId);
        FirstName = @event.FirstName;
        LastName = @event.LastName;
        DateOfBirth = @event.DateOfBirth;
        Email = @event.Email;
        PhoneNumber = @event.PhoneNumber;
        BankAccountNumber = @event.BankAccountNumber;
    }
    public void On(CustomerDeleted @event)
    {
        Id = Guid.Parse(@event.AggregateId);
        IsDeleted = @event.IsDeleted;
    }
}
