using Mc2.CrudTest.Application.Contracts.Persistence;

namespace Mc2.CrudTest.Application.Features.Customers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerEventStore customerEventStore;
    private readonly ICustomerRepository customerRepository;

    public DeleteCustomerCommandHandler(
        ICustomerEventStore customerEventStore,
        ICustomerRepository customerRepository)
    {
        this.customerEventStore = customerEventStore;
        this.customerRepository = customerRepository;
    }

    public async Task<Unit> Handle(
        DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await this.customerEventStore.RehydreateAsync(request.Id.ToString());
        customer.ValidateExistence(request.Id);

        Customer.Delete(customer);

        var t = await this.customerEventStore.GetTransaction();
        await t.RunInside(async () =>
        {
            await customerEventStore.SaveAsync(customer);
            await customerRepository.UpdateAsync(customer);
        });

        return Unit.Value;
    }
}
