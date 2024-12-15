using InventoryVenturus.Features.Transactions.Dtos;
using InventoryVenturus.Features.Transactions.Queries.GetDailyConsumption;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InventoryVenturus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TransactionsController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Gets the daily consumption for each product.
        /// </summary>
        /// <param name="date">The date to retrieve consumption for. If not specified, today's date will be used.</param>
        /// <returns>The list of daily consumption records.</returns>
        [HttpGet("daily-consumption")]
        [SwaggerResponse(StatusCodes.Status200OK, "The daily consumption was retrieved successfully.", typeof(IEnumerable<DailyConsumptionDto>))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No consumption records found for the specified date.", typeof(ProblemDetails))]
        public async Task<ActionResult<IEnumerable<DailyConsumptionDto>>> GetDailyConsumption([FromQuery] DateTime? date)
        {
            throw new NotImplementedException();
        }
    }
}
