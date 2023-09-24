using Mc2.CrudTest.Application.Features.Customers.Common;

namespace Mc2.CrudTest.Application.Features.Customers.Update;

public class UpdateCustomerCommand : CustomerCommand, IRequest<UpdateCustomerCommandResponse>
{
    public Guid Id { get; set; }
}
