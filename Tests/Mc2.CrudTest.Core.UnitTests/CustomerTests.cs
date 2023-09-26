using Mc2.CrudTest.Core.CustomerAggregate;
using Mc2.CrudTest.Core.CustomerAggregate.DomainEvents;
using Mc2.CrudTest.SharedKernel;
using Shouldly;
using Xunit;

namespace Mc2.CrudTest.Core.UnitTests;

public class CustomerTests
{
    private const string firstName = "ali";
    private const string lastName = "taheri";
    private const string email = "alit@email.com";
    private const string phoneNumber = "09123456987";
    private const string bankAccountNumber = "IR123456789";
    private readonly DateOnly dateOfBirth = new(1990, 01, 02);
    
    private const string id = "622a1b29-1636-46c5-a0c6-3aa60a65df28";

    private readonly Customer.Dto createDto;
    private readonly CustomerCreated createdEvent;

    public CustomerTests()
    {
        createDto = new Customer.Dto
        (
            FirstName: firstName,
            LastName: lastName,
            DateOfBirth: dateOfBirth,
            Email: email,
            PhoneNumber: phoneNumber,
            BankAccountNumber: bankAccountNumber
        );

        createdEvent = new CustomerCreated(id)
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            Email = email,
            PhoneNumber = phoneNumber,
            BankAccountNumber = bankAccountNumber
        };
    }

    [Fact]
    public void Create_Customer()
    {
        var newCustomer = Customer.Create(createDto);
        
        CheckCreation(newCustomer);
        newCustomer.DomainEvents.Count.ShouldBe(1);
    }
    [Fact]
    public void Create_With_Events()
    {
        var customer = new Customer(new List<DomainEvent> { createdEvent });

        CheckCreation(customer);
        customer.DomainEvents.Count.ShouldBe(0);
    }
    private static void CheckCreation(Customer newCustomer)
    {
        newCustomer.FirstName.ShouldBe("ali");
        newCustomer.LastName.ShouldBe("taheri");
        newCustomer.DateOfBirth.ShouldBe(new DateOnly(1990, 01, 02));
        newCustomer.Email.ShouldBe("alit@email.com");
        newCustomer.PhoneNumber.ShouldBe("09123456987");
        newCustomer.BankAccountNumber.ShouldBe("IR123456789");

        newCustomer.Id.ShouldNotBe(default);
        newCustomer.IsDeleted.ShouldBe(false);
    }

    [Fact]
    public void Update_Customer()
    {
        var customer = Customer.Create(createDto);

        var updateDto = new Customer.Dto
        (
            FirstName: firstName,
            LastName: lastName,
            DateOfBirth: new DateOnly(1990, 01, 03),
            Email: "alitaheri@email.com",
            PhoneNumber: "09123456988",
            BankAccountNumber: "IR123456788"
        );

        Customer.Update(customer, updateDto);
        
        CheckUpdate(customer, customer.Id);
        customer.DomainEvents.Count.ShouldBe(2);
    }
    [Fact]
    public void Update_With_Events()
    {
        var events = new List<DomainEvent>
        {
            createdEvent
        };

        var customerUpdated = new CustomerUpdated(id)
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = new DateOnly(1990, 01, 03),
            Email = "alitaheri@email.com",
            PhoneNumber = "09123456988",
            BankAccountNumber = "IR123456788"
        };

        events.Add(customerUpdated);

        var customer = new Customer(events);

        CheckUpdate(customer, Guid.Parse(id));
        customer.DomainEvents.Count.ShouldBe(0);
    }
    private static void CheckUpdate(Customer customer, Guid id)
    {
        customer.DateOfBirth.ShouldBe(new DateOnly(1990, 01, 03));
        customer.Email.ShouldBe("alitaheri@email.com");
        customer.PhoneNumber.ShouldBe("09123456988");
        customer.BankAccountNumber.ShouldBe("IR123456788");

        customer.Id.ShouldBeEquivalentTo(id);
        customer.IsDeleted.ShouldBe(false);
    }

    [Fact]
    public void Delete_Customer()
    {
        var customer = Customer.Create(createDto);

        Customer.Delete(customer);

        customer.IsDeleted.ShouldBe(true);
        customer.DomainEvents.Count.ShouldBe(2);
    }
    [Fact]
    public void Delete_With_Events()
    {
        var events = new List<DomainEvent>
        {
            createdEvent
        };

        var customerDeleted = new CustomerDeleted(id) { IsDeleted = true };

        events.Add(customerDeleted);

        var customer = new Customer(events);

        customer.IsDeleted.ShouldBe(true);
        customer.DomainEvents.Count.ShouldBe(0);
    }
}
