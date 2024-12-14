namespace InventoryVenturus.Features.Products.Dtos
{
    public record ProductDto(Guid Id, string Partnumber, string Name, decimal AveragePrice);
}
