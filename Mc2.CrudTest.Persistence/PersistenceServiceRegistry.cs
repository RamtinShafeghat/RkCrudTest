using Mc2.CrudTest.Persistence.EventStores;
using Mc2.CrudTest.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration, bool useDb = true)
    {
        if (useDb)
        {
            services.AddDbContext<IRayanKarDbContext, RayanKarDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RayanKarDbConnectionString")), ServiceLifetime.Transient);
        }

        services.AddTransient(typeof(IEventStore), typeof(EventStore));
        services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));

        services.AddTransient<ICustomerEventStore, CustomerEventStore>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
