namespace Mc2.CrudTest.Application.Features.Customers;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator(CustomerCommandInputValidator inputValidator,
                                          CustomerCommandBusinessValidator businessValidator) : base()
    {
        Include(inputValidator);
        Include(businessValidator);
    }
}
