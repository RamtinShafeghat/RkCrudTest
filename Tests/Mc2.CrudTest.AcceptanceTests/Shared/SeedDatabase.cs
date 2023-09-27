using Mc2.CrudTest.Persistence.EventStores;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.AcceptanceTests.Shared;

internal static class SeedDatabase
{
    public static Guid SeedId { get; private set; }

    public static void Seed(this ModelBuilder builder)
    {
        var customer = Customer.Create(new Customer.Dto(
            "alireza", 
            "davari", 
            new DateOnly(2000, 01, 01), 
            "alireza@email.com", 
            "09120556987", 
            "IR123456789"));

        var eventData = EventStore.GetEventData(customer.DomainEvents, customer.Version, customer.Id.ToString(), nameof(customer));

        builder.Entity<Customer>().HasData(customer);
        builder.Entity<EventData>().HasData(eventData);

        SeedId = customer.Id;
    }
}
