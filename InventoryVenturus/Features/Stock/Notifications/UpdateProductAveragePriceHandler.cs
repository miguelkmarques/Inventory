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

                var updatedAveragePrice = CalculateUpdatedAveragePrice(existingProduct.Price, notification.AddedQuantity, notification.FinalQuantity, notification.UnitPrice);

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

        private static decimal CalculateUpdatedAveragePrice(decimal currentPrice, int addedQuantity, int finalQuantity, decimal unitPrice)
        {
            var updatedAveragePrice = ((currentPrice * (finalQuantity - addedQuantity)) + (unitPrice * addedQuantity)) / finalQuantity;
            return Math.Round(updatedAveragePrice, 2);
        }
    }
}
