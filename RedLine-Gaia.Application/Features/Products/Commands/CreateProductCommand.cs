using FluentResults;
using Mapster;
using MediatR;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Errors;
using RedLine_Gaia.Domain.Interfaces;

namespace RedLine_Gaia.Application.Features.Products.Commands;

public record CreateProductCommand(ProductDTO dto) : IRequest<ResultDto<int>>;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, ResultDto<int>>
{
    public async Task<ResultDto<int>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        if (string.IsNullOrWhiteSpace(request.dto.Name))
            return Result.Fail<int>(new ProductNameEmptyError()).ToResultDto<int, int>();

        var product = request.dto.Adapt<Product>();

        if (!await productRepository.IsProductNameUnique(product))
            return Result.Fail<int>(new ProductNameMustBeUniqueError()).ToResultDto<int, int>();

        var result = await productRepository.Create(product);
        return result.ToResultDto<int, int>();
    }
}
