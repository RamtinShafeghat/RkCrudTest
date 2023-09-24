using Mc2.Crud.SharedKernel.Contracts;
using Mc2.CrudTest.Application.Contracts.Persistence;
using Mc2.CrudTest.Persistence.EventRepositories;
using Mc2.CrudTest.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RayanKarDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RayanKarDbConnectionString")), ServiceLifetime.Scoped);

        services.AddScoped(typeof(IEventStore), typeof(EventStore));
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        services.AddScoped<ICustomerEventStore, CustomerEventStore>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
