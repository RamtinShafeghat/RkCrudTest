using Mc2.CrudTest.Application.Contracts.Infrastructure;

namespace Mc2.CrudTest.Application.Features.Customers;

public class CustomerCommandInputValidator : AbstractValidator<CustomerCommand>
{
    private readonly IExternalValidator exValidator;

    public CustomerCommandInputValidator(IExternalValidator externalValidator) : base()
    {
        this.exValidator = externalValidator;
        Validate();
    }

    private void Validate()
    {
        RuleFor(customer => customer.Dto.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required")
            .NotEqual("string")
            .WithMessage("FirstName can not be default")
            .MaximumLength(50)
            .WithMessage("FirstName max length is 50");

        RuleFor(customer => customer.Dto.LastName)
            .NotEmpty()
            .WithMessage("LastName is required")
            .NotEqual("string")
            .WithMessage("LastName can not be default")
            .MaximumLength(50)
            .WithMessage("LastName max length is 50");

        RuleFor(customer => customer.Dto.DateOfBirth)
            .NotEmpty()
            .WithMessage("DateOfBirth is required")
            .Must(ValidateDateOfBirth)
            .WithMessage("DateOfBirth is invalid");

        RuleFor(customer => customer.Dto.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required")
            .Must(ValidatePhoneNumber)
            .WithMessage("PhoneNumber is invalid");

        RuleFor(customer => customer.Dto.Email)
            .NotEmpty()
            .WithMessage("EmailAddress is required")
            .EmailAddress()
            .WithMessage("EmailAddress is invalid");

        RuleFor(customer => customer.Dto.BankAccountNumber)
           .NotEmpty()
           .WithMessage("BankAccountNumber is required")
           .Must(ValidateBankAccountNumber)
           .WithMessage("BankAccountNumber can be (digit) or (digit with letter)");
    }

    private bool ValidateDateOfBirth(DateOnly dateOnly) =>
        dateOnly <= DateOnly.FromDateTime(DateTime.Now);
    private bool ValidatePhoneNumber(string phoneNumber) => 
        this.exValidator.ValidatePhoneNumber(phoneNumber);

    private bool ValidateBankAccountNumber(string accountNumber) =>
        accountNumber.All(char.IsLetterOrDigit) && accountNumber.Any(char.IsDigit) &&
        accountNumber.Length is >= 6 and <= 20;
}
