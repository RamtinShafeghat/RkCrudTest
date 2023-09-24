using Mc2.CrudTest.Application.Features.Customers.Common;

namespace Mc2.CrudTest.Application.Features.Customers.Create;

public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
{
    public CustomerCommandDto Dto { get; set; }
}
