using InventoryVenturus.Domain;
using InventoryVenturus.Features.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.TestData
{
    public static class ProductTestData
    {
        public static List<Product> GetSampleProducts => [
                new ("Product1", "PartNumber001") { Id = Guid.NewGuid(), Price = 100 },
                new ("Product2", "PartNumber002") { Id = Guid.NewGuid(), Price = 200 }
            ];

        public static Product GetSampleProduct => new("Product1", "PartNumber001") { Id = Guid.NewGuid(), Price = 100 };

        public static List<ProductDto> GetSampleProductDtos => [
                new(Guid.NewGuid(), "Product1", "PartNumber001", 100),
                new(Guid.NewGuid(), "Product2", "PartNumber002", 200)
            ];

        public static ProductDto GetSampleProductDto => new(Guid.NewGuid(), "Product1", "PartNumber001", 100);
    }
}
