﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Mc2.CrudTest.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var asm = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(asm);
        services.AddAutoMapper(asm);
        services.AddValidatorsFromAssembly(asm);

        return services;
    }
}
