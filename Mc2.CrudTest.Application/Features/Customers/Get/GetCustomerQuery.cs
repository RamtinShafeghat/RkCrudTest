namespace Mc2.CrudTest.Application.Features.Customers.Get;

public class GetCustomerQuery : IRequest<CustomerViewModel>
{
    public Guid Id { get; set; }
}
