using Mc2.CrudTest.Application.Features.Customers.Common;

namespace Mc2.CrudTest.Application.Features.Customers.Update;

public class UpdateCustomerCommandValidator : AbstractValidator<CustomerCommandDto>
{
    public UpdateCustomerCommandValidator()
    {
        Include(new CustomerCommandValidator());
    }
}
