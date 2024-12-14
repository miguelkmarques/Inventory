using InventoryVenturus.Features.Products.Dtos;
using MediatR;

namespace InventoryVenturus.Features.Products.Queries.List
{
    public record ListProductsQuery : IRequest<List<ProductDto>>;
}
