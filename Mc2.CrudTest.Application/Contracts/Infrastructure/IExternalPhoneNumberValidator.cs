namespace Mc2.CrudTest.Application.Contracts.Infrastructure;

public interface IExternalPhoneNumberValidator
{
    bool ValidatePhoneNumber(string phoneNumber);
}
