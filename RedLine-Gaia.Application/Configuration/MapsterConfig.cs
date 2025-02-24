using Mapster;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedLine_Gaia.Application.Configuration;

public class MapsterConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
        config.NewConfig<Product, ProductDTO>();
        config.NewConfig<ProductDTO,Product>();
    }
}