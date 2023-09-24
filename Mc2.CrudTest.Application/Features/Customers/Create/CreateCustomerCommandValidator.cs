using Mc2.CrudTest.Application.Features.Customers.Common;

namespace Mc2.CrudTest.Application.Features.Customers.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CustomerCommandDto>
{
    public CreateCustomerCommandValidator()
    {
        Include(new CustomerCommandValidator());
    }
}
