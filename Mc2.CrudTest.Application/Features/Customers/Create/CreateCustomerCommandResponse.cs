using Mc2.CrudTest.Application.Responses;

namespace Mc2.CrudTest.Application.Features.Customers.Create;

public class CreateCustomerCommandResponse : BaseResponse
{
    public Guid Id { get; set; }
}