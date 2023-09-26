using Mc2.CrudTest.AcceptanceTests.Customers.Commands;
using Mc2.CrudTest.Application.Contracts.Persistence;
using Mc2.CrudTest.Application.Features.Customers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TechTalk.SpecFlow.Assist;
using AppContext = Mc2.CrudTest.AcceptanceTests.Shared.AppContext;

namespace Mc2.CrudTest.AcceptanceTests.Features.Customer;

[Binding]
public class CreateACustomerSteps
{
    private readonly IMediator mediator;

    private readonly AppContext context;
    private readonly CreateCustomerCommand createCmd;

    public CreateACustomerSteps(AppContext context)
    {
        mediator = context.Container.GetService<IMediator>();

        this.context = context;
        createCmd = new CreateCustomerCommand();

    }

    [Given(@"the following customer info:")]
    public void GivenTheFollowingCustomerInfo(Table table)
    {
        var customerTable = table.CreateInstance<CreateCustomerTable>();
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
        await mediator.Send(this.createCmd);
    }

    [Then(@"the following customers record should be recorded:")]
    public async Task ThenTheFollowingCustomersRecordShouldBeRecorded(Table table)
    {
        var customerTable = table.CreateInstance<CreateCustomerTable>();

        var customerRepository = context.Container.GetService<ICustomerRepository>();

        var customer = (await customerRepository.ListAllAsync()).First();

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

