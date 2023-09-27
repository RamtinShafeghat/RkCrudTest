using Mc2.CrudTest.AcceptanceTests.Customers;
using Mc2.CrudTest.AcceptanceTests.Shared;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow.Assist;
using AppContext = Mc2.CrudTest.AcceptanceTests.Shared.AppContext;

namespace Mc2.CrudTest.AcceptanceTests.Features.Customer.Update;

[Binding]
public class UpdateCustomerSteps
{
    private readonly IMediator mediator;

    private readonly AppContext context;
    private readonly UpdateCustomerCommand updateCmd;

    public UpdateCustomerSteps(AppContext context)
    {
        mediator = context.Container.GetService<IMediator>();

        this.context = context;
        updateCmd = new UpdateCustomerCommand();

    }

    [Given(@"the following customer update info:")]
    public void GivenTheFollowingCustomerInfo(Table table)
    {
        var customerTable = table.CreateInstance<CustomerTable>();
        this.updateCmd.Dto = new CustomerCommandDto
        {
            FirstName = customerTable.FirstName,
            LastName = customerTable.LastName,
            BankAccountNumber = customerTable.BankAccountNumber,
            PhoneNumber = customerTable.PhoneNumber,
            Email = customerTable.Email,
            DateOfBirth = DateOnly.FromDateTime(customerTable.DateOfBirth)
        };
    }

    [When(@"I Update a customer")]
    public async Task WhenIUpdateACustomer()
    {
        updateCmd.Id = SeedDatabase.SeedId;
        await mediator.Send(this.updateCmd);
    }

    [Then(@"the following customer updated record should be recorded:")]
    public async Task ThenTheFollowingCustomerRecordShouldBeRecorded(Table table)
    {
        var customerTable = table.CreateInstance<CustomerTable>();

        var customerRepository = context.Container.GetService<ICustomerRepository>();

        var customer = (await customerRepository.ListAllAsync()).First(a => a.Id == SeedDatabase.SeedId);

        Assert.That(customer.FirstName,
            Is.EqualTo(customerTable.FirstName));

        Assert.That(customer.LastName,
            Is.EqualTo(customerTable.LastName));

        Assert.That(customer.PhoneNumber,
            Is.EqualTo(customerTable.PhoneNumber));

        Assert.That(customer.Email,
            Is.EqualTo(customerTable.Email));

        Assert.That(customer.BankAccountNumber,
            Is.EqualTo(customerTable.BankAccountNumber));

        Assert.That(customer.DateOfBirth,
            Is.EqualTo(DateOnly.FromDateTime(customerTable.DateOfBirth)));
    }

    [Then(@"the following customer updated record should be recorded in event store:")]
    public async Task ThenTheFollowingCustomerRecordShouldBeRecordedInEventStore(Table table)
    {
        var customerTable = table.CreateInstance<CustomerTable>();

        var customerEventStore = context.Container.GetService<ICustomerEventStore>();

        var customer = (await customerEventStore.RehydreateAsync(SeedDatabase.SeedId.ToString()));

        Assert.That(customer.FirstName,
            Is.EqualTo(customerTable.FirstName));

        Assert.That(customer.LastName,
            Is.EqualTo(customerTable.LastName));

        Assert.That(customer.PhoneNumber,
            Is.EqualTo(customerTable.PhoneNumber));

        Assert.That(customer.Email,
            Is.EqualTo(customerTable.Email));

        Assert.That(customer.BankAccountNumber,
            Is.EqualTo(customerTable.BankAccountNumber));

        Assert.That(customer.DateOfBirth,
            Is.EqualTo(DateOnly.FromDateTime(customerTable.DateOfBirth)));
    }
}

