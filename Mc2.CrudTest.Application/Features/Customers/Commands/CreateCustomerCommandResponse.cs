using Mc2.CrudTest.Application.Responses;

namespace Mc2.CrudTest.Application.Features.Customers.Commands;

public class CreateCustomerCommandResponse : BaseResponse
{
    public CreateCustomerCommandResponse() : base()
    {

    }

    public CreateCustomerDto Customer { get; set; }
}