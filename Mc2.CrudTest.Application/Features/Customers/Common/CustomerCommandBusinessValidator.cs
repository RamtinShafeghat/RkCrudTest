using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers;

public class CustomerCommandBusinessValidator : AbstractValidator<CustomerCommand>
{
    private readonly ICustomerRepository customerRepository;

    public CustomerCommandBusinessValidator(ICustomerRepository customerRepository) : base()
    {
        this.customerRepository = customerRepository;
        Validate();
    }

    private void Validate()
    {
        RuleFor(c => (c))
            .MustAsync(CheckEmailUniqueness)
            .WithMessage("Email is in use by another user");

        RuleFor(c => c)
            .MustAsync(CheckNamesAndBirthUniqueness)
            .WithMessage("FirstName, LastName and DateOfBirth is in use by another user");
    }
    private async Task<bool> CheckEmailUniqueness(CustomerCommand cmd, CancellationToken _)
    {
        return !await this.customerRepository
                          .ExistAsync(c => c.Id != cmd.ExistingCustomerId &&
                                           c.Email == cmd.Dto.Email);
    }

    private async Task<bool> CheckNamesAndBirthUniqueness(CustomerCommand cmd, CancellationToken _)
    {
        return !await this.customerRepository
                          .ExistAsync(c => c.Id != cmd.ExistingCustomerId &&
                                           c.FirstName == cmd.Dto.FirstName &&
                                           c.LastName == cmd.Dto.LastName &&
                                           c.DateOfBirth == cmd.Dto.DateOfBirth);
    }
}
