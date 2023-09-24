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

    public static Customer CreateCustomer(Dto dto)
    {
        var customer = new Customer();
        customer.Apply(new CustomerCreated(Guid.NewGuid().ToString())
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName, 
            DateOfBirth = dto.DateOfBirth, 
            Email = dto.Email, 
            PhoneNumber = dto.PhoneNumber,
            BankAccountNumber = dto.BankAccountNumber
        });

        return customer;
    }
    public static void UpdateCustomer(Customer existingCustomer,Dto dto)
    {
        existingCustomer.Apply(new CustomerUpdated(existingCustomer.Id.ToString())
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            BankAccountNumber = dto.BankAccountNumber
        });
    }
    public static void DeleteCustomer(Customer existingCustomer)
    {
        existingCustomer.Apply(new CustomerDeleted(existingCustomer.Id.ToString())
        {
            IsDeleted = true
        });
    }

    public void On(CustomerCreated @event)
    {
        OnCustomerCreatedOrUpdated(@event);
    }
    public void On(CustomerUpdated @event)
    {
        OnCustomerCreatedOrUpdated(@event);
    }
    public void On(CustomerDeleted @event)
    {
        Id = Guid.Parse(@event.AggregateId);
        IsDeleted = @event.IsDeleted;
    }
    
    private void OnCustomerCreatedOrUpdated(CustomerBaseEvent @event)
    {
        Id = Guid.Parse(@event.AggregateId);
        FirstName = @event.FirstName;
        LastName = @event.LastName;
        DateOfBirth = @event.DateOfBirth;
        Email = @event.Email;
        PhoneNumber = @event.PhoneNumber;
        BankAccountNumber = @event.BankAccountNumber;
    }

    public record Dto(
        string FirstName, 
        string LastName, 
        DateOnly DateOfBirth, 
        string Email,
        string PhoneNumber,
        string BankAccountNumber);
}
