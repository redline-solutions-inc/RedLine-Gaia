using Mapster;
using MediatR;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Domain.Interfaces;


namespace RedLine_Gaia.Application.Features.Products.Queries;

public record GetProductByIdQuery(int id) : IRequest<ProductDTO?>;

public class GetProductByIdQueryHandler
    : IRequestHandler<GetProductByIdQuery, ProductDTO?>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(
        IProductRepository productRepository
    )
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDTO?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellation
    )
    {
        var bulkReceipt = await _productRepository.GetProductById(
            request.id
        );

        return bulkReceipt.Adapt<ProductDTO?>();
    }
}
