using com.google.i18n.phonenumbers;
using Mc2.CrudTest.Application.Contracts.Infrastructure;
using static com.google.i18n.phonenumbers.Phonenumber;

namespace Mc2.CrudTest.Infrastructure._3rdParty;

public class GooglePhoneNumberValidator : IExternalPhoneNumberValidator
{
    public bool ValidatePhoneNumber(string phoneNumber)
    {
        PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.getInstance();

        PhoneNumber parsedNumber = phoneNumberUtil.parse(phoneNumber, null);
        return phoneNumberUtil.isValidNumber(parsedNumber);
    }
}
