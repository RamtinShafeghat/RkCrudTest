namespace Mc2.CrudTest.Application.Features.Customers;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator(CustomerCommandValidator validator) : base()
    {
        Include(validator);
    }
}
