using FluentResults;
using Mapster;
using MediatR;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Interfaces;

namespace RedLine_Gaia.Application.Features.Products.Queries;

public record GetProductByIdQuery(int id) : IRequest<ResultDto<ProductDTO>>;

public class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, ResultDto<ProductDTO>>
{
    public async Task<ResultDto<ProductDTO>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellation
    )
    {
        var result = await productRepository.GetProductById(request.id);

        return result.ToResultDto<Product, ProductDTO>();
    }
}
