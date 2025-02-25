using System.Diagnostics.CodeAnalysis;
using Mapster;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Domain.Entities;

namespace RedLine_Gaia.Application.Configuration;

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
