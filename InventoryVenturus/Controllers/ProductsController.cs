using InventoryVenturus.Features.Products.Commands.Create;
using InventoryVenturus.Features.Products.Dtos;
using InventoryVenturus.Features.Products.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryVenturus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="command">The command to create a new product.</param>
        /// <returns>The ID of the created product.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The product was created successfully.", typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid.", typeof(ProblemDetails))]
        public async Task<ActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }

        /// <summary>
        /// Gets a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product details.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The product was retrieved successfully.", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product was not found.", typeof(ProblemDetails))]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await mediator.Send(new GetProductQuery(id));
            if (product is null)
            {
                return NotFound();
            }
            return product;
        }
    }
}
