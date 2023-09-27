using Mc2.CrudTest.AcceptanceTests.Customers;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow.Assist;
using AppContext = Mc2.CrudTest.AcceptanceTests.Shared.AppContext;

namespace Mc2.CrudTest.AcceptanceTests.Features.Customer;

[Binding]
public class CreateACustomerSteps
{
    private readonly IMediator mediator;
    
    private readonly AppContext context;
    private readonly CreateCustomerCommand createCmd;
    
    private Guid createdId;

    public CreateACustomerSteps(AppContext context)
    {
        mediator = context.Container.GetService<IMediator>();
        
        this.context = context;
        createCmd = new CreateCustomerCommand();

    }

    [Given(@"the following customer info:")]
    public void GivenTheFollowingCustomerInfo(Table table)
    {
        var customerTable = table.CreateInstance<CustomerTable>();
        this.createCmd.Dto = new CustomerCommandDto
        {
            FirstName = customerTable.FirstName,
            LastName = customerTable.LastName,
            BankAccountNumber = customerTable.BankAccountNumber,
            PhoneNumber = customerTable.PhoneNumber,
            Email = customerTable.Email,
            DateOfBirth = DateOnly.FromDateTime(customerTable.DateOfBirth)
        };
    }

    [When(@"I create a customer")]
    public async Task WhenICreateACustomer()
    {
        createdId = (await mediator.Send(this.createCmd)).Id;
    }

    [Then(@"the following customers record should be recorded:")]
    public async Task ThenTheFollowingCustomersRecordShouldBeRecorded(Table table)
    {
        var customerTable = table.CreateInstance<CustomerTable>();

        var customerRepository = context.Container.GetService<ICustomerRepository>();

        var customer = (await customerRepository.ListAllAsync()).First(a => a.Id == createdId);

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

