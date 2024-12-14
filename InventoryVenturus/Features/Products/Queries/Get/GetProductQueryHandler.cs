using InventoryVenturus.Features.Products.Dtos;
using InventoryVenturus.Repositories.Interfaces;
using MediatR;
using System;

namespace InventoryVenturus.Features.Products.Queries.Get
{
    public class GetProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductQuery, ProductDto?>
    {
        public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductByIdAsync(request.Id);
            if (product is null)
            {
                return null;
            }
            return new ProductDto(product.Id, product.Partnumber, product.Name, product.Price);
        }
    }
}
