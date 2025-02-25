namespace RedLine_Gaia.Application.Features.Products.DTOs;

/// <summary>
/// Base DTO for the Product.
/// </summary>
public class ProductDTO
{
    public int? Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}
