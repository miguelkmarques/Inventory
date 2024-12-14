using InventoryVenturus.Features.Products.Dtos;
using MediatR;

namespace InventoryVenturus.Features.Products.Queries.Get
{
    public record GetProductQuery(Guid Id) : IRequest<ProductDto>;
}
