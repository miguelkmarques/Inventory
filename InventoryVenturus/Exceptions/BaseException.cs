using System.Net;

namespace InventoryVenturus.Exceptions
{
    public class BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : Exception(message)
    {
        public HttpStatusCode StatusCode { get; } = statusCode;
    }

    public class ProductNotFoundException(Guid id) : BaseException($"Product with ID {id} not found", HttpStatusCode.NotFound)
    {
    }

    public class InsufficientStockException(Guid productId, int requestedQuantity, int availableQuantity) : BaseException($"Insufficient stock for product ID: {productId}. Requested: {requestedQuantity}, Available: {availableQuantity}", HttpStatusCode.BadRequest)
    {
    }
}
