using InventoryVenturus.Features.Products.Commands.Create;
using InventoryVenturus.Features.Products.Commands.Delete;
using InventoryVenturus.Features.Products.Commands.Update;
using InventoryVenturus.Features.Products.Dtos;
using InventoryVenturus.Features.Products.Queries.Get;
using InventoryVenturus.Features.Products.Queries.List;
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
            return Ok(product);
        }

        /// <summary>
        /// Lists all products.
        /// </summary>
        /// <returns>A list of products.</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "The products were retrieved successfully.", typeof(IEnumerable<ProductDto>))]
        public async Task<ActionResult<IEnumerable<ProductDto>>> ListProducts()
        {
            var products = await mediator.Send(new ListProductsQuery());
            return Ok(products);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="command">The command to update the product.</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was updated successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid.", typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product was not found.", typeof(ProblemDetails))]
        public async Task<ActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            var result = await mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a product by ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The product was deleted successfully.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product was not found.", typeof(ProblemDetails))]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            var result = await mediator.Send(new DeleteProductCommand(id));
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
