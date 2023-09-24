using Mc2.CrudTest.Application.Features.Customers.Get;

namespace Mc2.CrudTest.Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Customer, CustomerVM>().ReverseMap();
    }
}
