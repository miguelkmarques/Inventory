using MediatR;
using System.Text.Json;

namespace InventoryVenturus.Behaviors
{
    public class RequestResponseLoggingBehavior<TRequest, TResponse>(ILogger<RequestResponseLoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid();

            // Request Logging
            // Log the serialized request
            logger.LogInformation("Handling request {CorrelationID}: {Request}", correlationId, request);

            // Response logging
            var response = await next();
            // Serialize the response
            // Log the serialized response
            logger.LogInformation("Response for {Correlation}: {Response}", correlationId, response);

            // Return response
            return response;
        }
    }
}
