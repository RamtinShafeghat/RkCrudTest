namespace Mc2.CrudTest.Application.Features.Customers;

public class DeleteCustomerCommand : CustomerCommandDto, IRequest
{
    public Guid Id { get; set; }
}
