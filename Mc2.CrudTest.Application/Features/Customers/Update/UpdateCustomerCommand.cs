using Mc2.CrudTest.Application.Features.Customers.Common;

namespace Mc2.CrudTest.Application.Features.Customers.Update;

public class UpdateCustomerCommand : IRequest<UpdateCustomerCommandResponse>
{
    public Guid Id { get; set; }

    public CustomerCommandDto Dto { get; set; }
}
