using Mc2.CrudTest.Application.Contracts.Infrastructure;
using PhoneNumbers;

namespace Mc2.CrudTest.Infrastructure._3rdParty;

public class GooglePhoneNumberValidator : IExternalPhoneNumberValidator
{
    public bool ValidatePhoneNumber(string phoneNumber)
    {
        PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        PhoneNumber parsedNumber = phoneNumberUtil.Parse(phoneNumber, null);
        return phoneNumberUtil.IsValidNumber(parsedNumber);
    }
}
