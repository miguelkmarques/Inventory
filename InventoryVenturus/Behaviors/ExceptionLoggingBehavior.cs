using InventoryVenturus.Data.Interfaces;
using InventoryVenturus.Domain;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using System.Text.Json;

namespace InventoryVenturus.Behaviors
{
    public class ExceptionLoggingBehavior<TRequest, TResponse>(IErrorLogRepository errorLogRepository)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var correlationId = Guid.NewGuid();
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestType = typeof(TRequest).Name;
                var requestJson = JsonSerializer.Serialize(request);
                var exceptionString = ex.ToString();
                await errorLogRepository.AddErrorLogAsync(new ErrorLog(correlationId, requestType, requestJson, exceptionString));
                throw;
            }
        }
    }
}
