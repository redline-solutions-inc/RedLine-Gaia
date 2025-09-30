using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedLine_Gaia.Application.Features.Products.Commands;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.Features.Products.Queries;
using RedLine_Gaia.Application.ResultDto;
using RedLine_Gaia.WebApi.Middleware;

namespace RedLine_Gaia.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ServiceFilter(typeof(TenantActionFilter))]
    [Authorize]
    [Tags("Product Management")]
    public class ProductController(ISender sender) : ControllerBase
    {
        [HttpPost(Name = "CreateProduct")]
        [EndpointSummary("CreateProduct")]
        [EndpointDescription("This endpoint creates a new Product.")]
        public async Task<ResultDto<int>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await sender.Send(command);
            return result;
        }

        [HttpGet(Name = "GetProductById")]
        [EndpointSummary("GetProductById")]
        [EndpointDescription("This endpoint fetches a Product by a given Id.")]
        public async Task<ResultDto<ProductDTO>> GetProductById(
            [FromQuery] GetProductByIdQuery query
        )
        {
            var result = await sender.Send(query);
            return result;
        }
    }
}
