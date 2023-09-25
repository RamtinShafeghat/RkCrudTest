namespace Mc2.CrudTest.Application.Features.Customers;

public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
{
    public CustomerCommandDto Dto { get; set; }
}
