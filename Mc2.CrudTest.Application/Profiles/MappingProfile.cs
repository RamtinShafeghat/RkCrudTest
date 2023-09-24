using Mc2.CrudTest.Application.Features.Customers.Common;
using Mc2.CrudTest.Application.Features.Customers.Get;

namespace Mc2.CrudTest.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerViewModel>().ReverseMap();
        CreateMap<CustomerCommandDto, Customer.Dto>().ReverseMap();
    }
}
