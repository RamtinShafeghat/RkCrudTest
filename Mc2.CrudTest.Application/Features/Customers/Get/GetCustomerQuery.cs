namespace Mc2.CrudTest.Application.Features.Customers.Get;

public class GetCustomerQuery : IRequest<CustomerVM>
{
    public Guid Id { get; set; }
}
