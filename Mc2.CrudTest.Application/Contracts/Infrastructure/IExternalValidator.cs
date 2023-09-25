namespace Mc2.CrudTest.Application.Contracts.Infrastructure;

public interface IExternalValidator
{
    bool ValidatePhoneNumber(string phoneNumber);
}
