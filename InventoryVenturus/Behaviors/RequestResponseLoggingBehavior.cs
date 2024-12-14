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
            // Serialize the request
            var requestType = typeof(TRequest).Name;
            var requestJson = JsonSerializer.Serialize(request);
            // Log the serialized request
            logger.LogInformation("Handling request {CorrelationID}: {RequestType} {Request}", correlationId, requestType, requestJson);

            // Response logging
            var response = await next();
            // Serialize the response
            var responseType = typeof(TResponse).Name;
            var responseJson = JsonSerializer.Serialize(response);
            // Log the serialized response
            logger.LogInformation("Response for {CorrelationID}: {ResponseType} {Response}", correlationId, responseType, responseJson);

            // Return response
            return response;
        }
    }
}
