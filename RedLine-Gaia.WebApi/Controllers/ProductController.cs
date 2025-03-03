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
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ResultDto<int>> CreateProduct(CreateProductCommand command)
        {
            var result = await _sender.Send(command);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResultDto<ProductDTO>> GetProductById(int id)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id));
            return result;
        }
    }
}
