using Mc2.CrudTest.Application.Contracts.Persistence;
using Mc2.CrudTest.Application.Exceptions;

namespace Mc2.CrudTest.Application.Features.Customers.Delete;

public class DeleteCustomerCommandHandler :
    IRequestHandler<DeleteCustomerCommand>
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

        Customer.DeleteCustomer(customer);

        await customerEventStore.SaveAsync(customer);
        await customerRepository.DeleteAsync(customer);

        return Unit.Value;
    }
}
