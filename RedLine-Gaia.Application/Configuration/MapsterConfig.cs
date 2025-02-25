using System.Diagnostics.CodeAnalysis;
using Mapster;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Domain.Entities;

namespace RedLine_Gaia.Application.Configuration;

/// <summary>
/// Base config class for all Mapster mappings.
/// </summary>
/// <remarks>
/// Mapster gerates, at runtime, in memory mappers of defined object relationships.
/// All entity to DTO mappings should be configured here.
/// </remarks>
[ExcludeFromCodeCoverage]
public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
        config.NewConfig<Product, ProductDTO>();
        config.NewConfig<ProductDTO, Product>();
    }
}
