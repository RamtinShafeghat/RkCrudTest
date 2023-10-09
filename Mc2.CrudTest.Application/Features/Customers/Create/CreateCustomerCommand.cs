namespace Mc2.CrudTest.Application.Features.Customers;

public class CreateCustomerCommand : CustomerCommand, IRequest<CreateCustomerCommandResponse>
{
    public override Guid ExistingCustomerId => default;
}
