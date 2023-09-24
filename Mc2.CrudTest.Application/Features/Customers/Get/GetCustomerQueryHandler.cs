using Mc2.CrudTest.Application.Contracts.Persistence;
using Mc2.CrudTest.Application.Exceptions;

namespace Mc2.CrudTest.Application.Features.Customers.Get;

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
        if (customer == null)
            throw new NotFoundException(nameof(customer), request.Id);
        if (customer.IsDeleted)
            throw new BadRequestException($"Customer {request.Id} has already deleted");

        return this.mapper.Map<CustomerViewModel>(customer);
    }
}
