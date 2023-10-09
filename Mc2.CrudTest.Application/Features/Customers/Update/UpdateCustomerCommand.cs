namespace Mc2.CrudTest.Application.Features.Customers;

public class UpdateCustomerCommand : CustomerCommand, IRequest
{
    public Guid Id { get; set; }
}
