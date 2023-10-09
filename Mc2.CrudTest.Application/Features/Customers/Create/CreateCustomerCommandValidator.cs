namespace Mc2.CrudTest.Application.Features.Customers;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator(CustomerCommandInputValidator inputValidator,
                                          CustomerCommandBusinessValidator businessValidator) : base()
    {
        Include(inputValidator);
        Include(businessValidator);
    }
}
