﻿using Microsoft.Extensions.DependencyInjection;
using RedLine_Gaia.Domain.Interfaces;
using RedLine_Gaia.Infrastructure.Repositories;
using RedLine_Gaia.Infrastructure.UoW;

namespace RedLine_Gaia.Infrastructure;

/// <summary>
/// Helper to manage dependency inject in the Infrastructure project.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
