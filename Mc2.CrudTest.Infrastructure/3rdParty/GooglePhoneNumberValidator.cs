using Mc2.CrudTest.Application.Contracts.Infrastructure;
using PhoneNumbers;

namespace Mc2.CrudTest.Infrastructure._3rdParty;

public class GooglePhoneNumberValidator : IExternalValidator
{
    public bool ValidatePhoneNumber(string phoneNumber)
    {
        _ = ulong.TryParse(phoneNumber, out var phoneNumberLong);

        PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        var ph = new PhoneNumber().CreateBuilderForType()
                                  .SetNationalNumber(phoneNumberLong)
                                  .SetCountryCode(98)
                                  .Build();

        return phoneNumberUtil.IsValidNumber(ph);
    }
}
