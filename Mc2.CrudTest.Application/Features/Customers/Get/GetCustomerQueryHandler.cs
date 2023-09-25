using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerViewModel>
{
    private readonly IMapper mapper;
    private readonly ICustomerRepository customerRepository;

    public GetCustomerQueryHandler(
        IMapper mapper,
        ICustomerRepository customerRepository)
    {
        this.mapper = mapper;
        this.customerRepository = customerRepository;
    }

    async Task<CustomerViewModel> IRequestHandler<GetCustomerQuery, CustomerViewModel>.Handle(
        GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var customer = await this.customerRepository.GetByIdAsync(request.Id);
        customer.ValidateExistence(request.Id);

        return this.mapper.Map<CustomerViewModel>(customer);
    }
}
