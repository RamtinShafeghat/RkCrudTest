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

    public static Customer Create(Dto createDto)
    {
        var customer = new Customer();
        customer.Apply(new CustomerCreated(Guid.NewGuid().ToString())
        {
            FirstName = createDto.FirstName,
            LastName = createDto.LastName, 
            DateOfBirth = createDto.DateOfBirth, 
            Email = createDto.Email, 
            PhoneNumber = createDto.PhoneNumber,
            BankAccountNumber = createDto.BankAccountNumber
        });

        return customer;
    }
    public static void Update(Customer existing, Dto updateDto)
    {
        existing.Apply(new CustomerUpdated(existing.Id.ToString())
        {
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName,
            DateOfBirth = updateDto.DateOfBirth,
            Email = updateDto.Email,
            PhoneNumber = updateDto.PhoneNumber,
            BankAccountNumber = updateDto.BankAccountNumber
        });
    }
    public static void Delete(Customer existing)
    {
        existing.Apply(new CustomerDeleted(existing.Id.ToString())
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
