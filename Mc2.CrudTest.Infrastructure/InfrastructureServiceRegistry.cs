using Mc2.CrudTest.Application.Contracts.Infrastructure;
using Mc2.CrudTest.Infrastructure._3rdParty;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IExternalValidator, GooglePhoneNumberValidator>();

        return services;
    }
}
