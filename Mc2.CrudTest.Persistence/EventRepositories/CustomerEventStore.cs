namespace Mc2.CrudTest.Persistence.EventRepositories;

internal class CustomerEventStore : ICustomerEventStore
{
    private readonly IEventStore eventStore;
    public CustomerEventStore(IEventStore eventStore)
    {
        this.eventStore = eventStore;
    }

    public async Task<Customer> RehydreateAsync(string id)
    {
        var customerEvents = await eventStore.LoadEventsAsync(id);

        return customerEvents.Count > 0 ? new Customer(customerEvents) : null;
    }

    public async Task SaveAsync(Customer customer)
    {
        await eventStore.SaveEventsAsync(customer.DomainEvents, customer.Version, customer.Id.ToString(), nameof(Customer));
    }
}
