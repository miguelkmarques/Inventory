using InventoryVenturus.Exceptions;
using InventoryVenturus.Features.Products.Notifications;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Notifications
{
    public class UpdateProductAveragePriceHandler(ILogger<UpdateProductAveragePriceHandler> logger, IProductRepository productRepository) : INotificationHandler<StockAddedNotification>
    {
        public async Task Handle(StockAddedNotification notification, CancellationToken cancellationToken)
        {
            try
            {
                var existingProduct = await productRepository.GetProductByIdAsync(notification.ProductId) ?? throw new ProductNotFoundException(notification.ProductId);

                var updatedAveragePrice = ((existingProduct.Price * (notification.FinalQuantity - notification.AddedQuantity)) + (notification.Price * notification.AddedQuantity)) / notification.FinalQuantity;

                var result = await productRepository.UpdateAveragePriceAsync(existingProduct.Id, updatedAveragePrice);
                if (!result)
                {
                    throw new ProductNotFoundException(notification.ProductId);
                }
                logger.LogInformation("Product Average Price updated for product with ID: {Id}", notification.ProductId);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating Product Average Price for product with ID: {Id}", notification.ProductId);
                throw;
            }
        }
    }
}
