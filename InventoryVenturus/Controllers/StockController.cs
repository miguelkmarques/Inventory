using InventoryVenturus.Domain;
using InventoryVenturus.Features.Stock.Commands.Add;
using InventoryVenturus.Features.Stock.Commands.Consume;
using InventoryVenturus.Features.Stock.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryVenturus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StockController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Gets the stock quantity for a product by ID.
        /// </summary>
        /// <param name="productId">The ID of the product to retrieve stock for.</param>
        /// <returns>The stock quantity.</returns>
        [HttpGet("{productId}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The stock was retrieved successfully.", typeof(int))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The product was not found.", typeof(ProblemDetails))]
        public async Task<ActionResult<int>> GetStock(Guid productId)
        {
            var stock = await mediator.Send(new GetStockQuery(productId));
            if (stock is null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        /// <summary>
        /// Adds stock for a product.
        /// </summary>
        /// <param name="command">The command to add stock.</param>
        /// <returns>No content.</returns>
        [HttpPost("add")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The stock was added successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid.", typeof(ProblemDetails))]
        public async Task<ActionResult> AddStock([FromBody] AddStockCommand command)
        {
            var result = await mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Consumes stock for a product.
        /// </summary>
        /// <param name="command">The command to consume stock.</param>
        /// <returns>No content.</returns>
        [HttpPost("consume")]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The stock was consumed successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The request is invalid.", typeof(ProblemDetails))]
        public async Task<ActionResult> ConsumeStock([FromBody] ConsumeStockCommand command)
        {
            try
            {
                var result = await mediator.Send(command);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
