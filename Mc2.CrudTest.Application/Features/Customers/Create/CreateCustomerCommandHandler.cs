using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly IMapper mapper;
    private readonly ICustomerEventStore eventStore;
    private readonly ICustomerRepository repository;

    public CreateCustomerCommandHandler(
        IMapper mapper,
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository)
    {
        this.mapper = mapper;
        this.eventStore = customerEventStore;
        this.repository = customerRepository;
    }

    public async Task<CreateCustomerCommandResponse> Handle(
        CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customerDto = this.mapper.Map<Customer.Dto>(request.Dto);
        var customer = Customer.Create(customerDto);

        var t = await this.eventStore.GetTransaction();
        await t.RunInside(async () =>
        {
            await eventStore.SaveAsync(customer);
            customer = await repository.AddAsync(customer);
        });

        return new CreateCustomerCommandResponse
        {
            Id = customer.Id
        };
    }
}
