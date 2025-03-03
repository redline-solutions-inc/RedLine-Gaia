using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedLine_Gaia.Application.Features.Products.Commands;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.Features.Products.Queries;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.WebApi.Middleware;

namespace RedLine_Gaia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(TenantActionFilter))]
    public class ProductController(ISender sender) : ControllerBase
    {
        [HttpPost(Name = "CreateProduct")]
        [EndpointSummary("CreateProduct")]
        [EndpointDescription("This endpoint creates a new Product.")]
        public async Task<ResultDto<int>> CreateProduct(CreateProductCommand command)
        {
            var result = await sender.Send(command);
            return result;
        }

        [HttpGet("{id:int}", Name = "GetProductById")]
        [EndpointSummary("GetProductById")]
        [EndpointDescription("This endpoint fetches a Product by a given Id.")]
        public async Task<ResultDto<ProductDTO>> GetProductById(int id)
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            return result;
        }
    }
}
