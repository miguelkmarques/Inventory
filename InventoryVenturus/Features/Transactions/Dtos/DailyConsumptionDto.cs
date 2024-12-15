namespace InventoryVenturus.Features.Transactions.Dtos
{
    public record DailyConsumptionDto(Guid ProductId, int Quantity, decimal Price);
}
