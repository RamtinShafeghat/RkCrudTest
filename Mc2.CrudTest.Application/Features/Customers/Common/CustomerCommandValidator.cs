using Mc2.CrudTest.Application.Contracts.Infrastructure;

namespace Mc2.CrudTest.Application.Features.Customers.Common;

public class CustomerCommandValidator : AbstractValidator<CustomerCommand>
{
    private readonly IExternalPhoneNumberValidator exValidator;

    public CustomerCommandValidator(IExternalPhoneNumberValidator exValidator)
    {
        this.exValidator = exValidator;
    }

    public CustomerCommandValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required")
            .Must(ValidatePhoneNumber)
            .WithMessage("PhoneNumber is invalid");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("EmailAddress is required")
            .EmailAddress()
            .WithMessage("EmailAddress is invalid");

        RuleFor(x => x.BankAccountNumber)
           .NotEmpty()
           .WithMessage("BankAccountNumber is required")
           .Must(ValidateBankAccountNumber)
           .WithMessage("BankAccountNumber is invalid");
    }

    private bool ValidatePhoneNumber(string phoneNumber) => 
        this.exValidator.ValidatePhoneNumber(phoneNumber);
    private bool ValidateBankAccountNumber(string accountNumber) => 
        accountNumber.Length is >= 6 and <= 20;
}
