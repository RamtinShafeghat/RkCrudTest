using Mc2.CrudTest.Application.Features.Customers.Common;

namespace Mc2.CrudTest.Application.Features.Customers.Delete;

public class DeleteCustomerCommand : CustomerCommand, IRequest
{
    public Guid Id { get; set; }
}
