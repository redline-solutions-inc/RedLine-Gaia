using Mapster;
using MediatR;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Domain.Entities;
using RedLine_Gaia.Domain.Interfaces;

namespace RedLine_Gaia.Application.Features.Products.Commands;

public record CreateProductCommand(ProductDTO dto) : IRequest<int>;

public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<int> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        var product = request.dto.Adapt<Product>();
        var result = await _productRepository.Create(product);
        return result;
       
    }
}
