using InventoryVenturus.Features.Products.Dtos;
using InventoryVenturus.Features.Products.Queries.Get;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Products.Queries.List
{
    public class ListProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<ListProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var productsQuery = await productRepository.GetAllProductsAsync();

            return productsQuery.Select(product => new ProductDto(product.Id, product.Partnumber, product.Name, product.Price)).ToList();
        }
    }
}
