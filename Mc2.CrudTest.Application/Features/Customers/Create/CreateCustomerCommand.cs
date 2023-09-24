using Mc2.CrudTest.Application.Features.Customers.Common;

namespace Mc2.CrudTest.Application.Features.Customers.Create;

public class CreateCustomerCommand : CustomerCommand, IRequest<CreateCustomerCommandResponse>
{
}
