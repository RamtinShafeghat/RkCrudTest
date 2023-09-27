using Mc2.CrudTest.AcceptanceTests.Customers;
using Mc2.CrudTest.AcceptanceTests.Shared;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow.Assist;
using AppContext = Mc2.CrudTest.AcceptanceTests.Shared.AppContext;

namespace Mc2.CrudTest.AcceptanceTests.Features.Customer.GetItem;

[Binding]
public class GetCustomerSteps
{
    private readonly IMediator mediator;
    private readonly AppContext context;
    
    private CustomerViewModel fetchedCustomer;

    public GetCustomerSteps(AppContext context)
    {
        this.context = context;
        mediator = context.Container.GetService<IMediator>();
    }

    [When(@"I request a default customer")]
    public async Task WhenIRequestAListOfCustomers()
    {
        fetchedCustomer = (await mediator.Send(new GetCustomerQuery() { Id = SeedDatabase.SeedId }));
    }

    [Then(@"the following customer should be returned:")]
    public void ThenTheFollowingCustomersShouldBeReturned(Table table)
    {
        var model = table.CreateSet<CustomerTable>().First();

        Assert.That(fetchedCustomer.FirstName,
            Is.EqualTo(model.FirstName));

        Assert.That(fetchedCustomer.LastName,
            Is.EqualTo(model.LastName));

        Assert.That(fetchedCustomer.Email,
            Is.EqualTo(model.Email));

        Assert.That(fetchedCustomer.PhoneNumber,
            Is.EqualTo(model.PhoneNumber));

        Assert.That(fetchedCustomer.Email,
            Is.EqualTo(model.Email));

        Assert.That(fetchedCustomer.bankAccountNumber,
            Is.EqualTo(model.BankAccountNumber));
    }
}

