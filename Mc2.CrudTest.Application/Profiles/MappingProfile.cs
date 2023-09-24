using Mc2.CrudTest.Application.Features.Customers.Commands;

namespace Mc2.CrudTest.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CreateCustomerCommand>();
    }
}
