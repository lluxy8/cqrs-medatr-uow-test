using Application.Commands.ProductCommands;
using Application.Commands.UserCommands;
using Application.Queries.ProductQueries;
using Application.Queries.UserQueries;
using Core.DTOs;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
            return Ok(products);
        }

        [HttpGet("with-user")]
        public async Task<IActionResult> GetAllProductsWithUser(CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetAllProductsWithUserQuery(), cancellationToken);
            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpGet("{id:guid}/with-products")]
        public async Task<IActionResult> GetProductByIdWithUser(Guid id, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductByIdWithUserQuery(id), cancellationToken);
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto dto, CancellationToken cancellationToken)
        {
            var productResult = await _mediator.Send(new CreateProductCommand(dto), cancellationToken);
            if (productResult.Data == Guid.Empty)
                return NotFound(productResult.Message);

            return CreatedAtAction(nameof(GetProductById), new { id = productResult.Data }, new { Id = productResult.Data });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductViewModel dto, CancellationToken cancellationToken)
        {
            var updatedProductResult = await _mediator.Send(new UpdateProductCommand(id, dto), cancellationToken);
            return updatedProductResult.Data == Guid.Empty ? NotFound(updatedProductResult.Message) : Ok(updatedProductResult.Data);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            var deleteResult = await _mediator.Send(new RemoveProductCommand(id), cancellationToken);
            return deleteResult.Data ? NoContent() : NotFound(deleteResult.Message);
        }
    }
}
