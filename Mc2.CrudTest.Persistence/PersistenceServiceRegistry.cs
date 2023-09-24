using Mc2.Crud.SharedKernel.Contracts;
using Mc2.CrudTest.Application.Contracts.Persistence;
//using Mc2.CrudTest.Persistence.EventRepositories;
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

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        //services.AddScoped(typeof(IEventRepository<,>), typeof(EventRepository<,>));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        //services.AddScoped<ICustomerEventRepository, CustomerEventRepository>();

        return services;
    }
}
