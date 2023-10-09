namespace Mc2.CrudTest.Application.Features.Customers;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(CustomerCommandValidator validator) : base()
    {
        Include(validator);
    }
}
