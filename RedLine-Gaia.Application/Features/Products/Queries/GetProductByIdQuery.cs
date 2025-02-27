using FluentResults;
using MediatR;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Errors;
using RedLine_Gaia.Domain.Interfaces;

namespace RedLine_Gaia.Application.Features.Products.Queries;

/// <summary>
/// MediatR Query to get a product by Id.
/// </summary>
/// <param name="id"></param>
public record GetProductByIdQuery(int id) : IRequest<ResultDto<ProductDTO>>;

/// <summary>
/// MediatR Query Handler for the GetProductByIdQuery.
/// </summary>
/// <param name="productRepository">Product Repository</param>
public class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, ResultDto<ProductDTO>>
{
    public async Task<ResultDto<ProductDTO>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellation
    )
    {
        var product = await productRepository.GetById(request.id);

        if (product is null)
        {
            return Result
                .Fail<Product>(new ProductNotFoundError())
                .ToResultDto<Product, ProductDTO>();
        }

        return Result.Ok(product).ToResultDto<Product, ProductDTO>();
    }
}
