//using Mc2.CrudTest.Application.Contracts.Persistence;
//using Mc2.CrudTest.Core.CustomerAggregate;
//using System;

//namespace Mc2.CrudTest.Persistence.EventRepositories;

//internal class CustomerEventRepository : EventRepository<Customer, Guid>, ICustomerEventRepository
//{
//    private readonly IEventStore<Customer, Guid> eventStore;

//    public CustomerEventRepository(IEventStore<Customer, Guid> eventStore) : base(eventStore)
//    {
//        this.eventStore = eventStore;
//    }

//    public async Task<Customer> GetPerson(string id)
//    {
//        var customerEvents = await eventStore.LoadAsync(Guid.Parse(id));

//        return customerEvents.Count > 0 ? new Customer(customerEvents) : null;
//    }

//    public async Task<Guid> SavePersonAsync(Customer customer)
//    {
//        await eventStore.SaveAsync(customer.Id, customer.Version, customer.DomainEvents);
//        return customer.Id;
//    }
//}
