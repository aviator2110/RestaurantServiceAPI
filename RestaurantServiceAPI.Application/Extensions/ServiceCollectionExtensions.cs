using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantServiceAPI.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicaton(this IServiceCollection services)
    {
        services.AddMediatR(
            config =>
            config.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));

        return services;
    }
}
