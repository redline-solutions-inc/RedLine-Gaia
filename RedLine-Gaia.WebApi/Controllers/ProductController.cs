using MediatR;
using Microsoft.AspNetCore.Mvc;
using RedLine_Gaia.Application.Features.Products.Commands;
using RedLine_Gaia.Application.Features.Products.DTOs;
using RedLine_Gaia.Application.Features.Products.Queries;

namespace RedLine_Gaia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(
            CreateProductCommand command
        )
        {
            var dto = await _sender.Send(command);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _sender.Send(new GetProductByIdQuery(id));

            return result is null ? NotFound() : Ok(result);
        }
    }
}
