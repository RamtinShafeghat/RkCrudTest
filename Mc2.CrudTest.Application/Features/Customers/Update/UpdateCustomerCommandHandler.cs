using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IMapper mapper;
    private readonly ICustomerEventStore eventStore;
    private readonly ICustomerRepository repository;

    public UpdateCustomerCommandHandler(
        IMapper mapper,
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository)
    {
        this.mapper = mapper;
        this.eventStore = customerEventStore;
        this.repository = customerRepository;
    }

    public async Task<Unit> Handle(
        UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await this.eventStore.RehydreateAsync(request.Id.ToString());
        customer.ValidateExistence(request.Id);

        var customerDto = this.mapper.Map<Customer.Dto>(request.Dto);
        Customer.Update(customer, customerDto);

        var t = await this.eventStore.GetTransaction();
        await t.RunInside(async () =>
        {
            await eventStore.SaveAsync(customer);
            await repository.UpdateAsync(customer);
        });

        return Unit.Value;
    }
}
