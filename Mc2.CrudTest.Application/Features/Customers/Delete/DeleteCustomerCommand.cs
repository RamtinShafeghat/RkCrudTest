namespace Mc2.CrudTest.Application.Features.Customers;

public class DeleteCustomerCommand : IRequest
{
    public Guid Id { get; set; }
}
