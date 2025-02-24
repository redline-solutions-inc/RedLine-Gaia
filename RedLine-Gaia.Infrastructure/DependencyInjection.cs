using Microsoft.Extensions.DependencyInjection;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure.Repositories;

namespace RedLine_Gaia.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        return services;
    }
}
