using InventoryVenturus.Features.Products.Commands.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Commands.Update
{
    public class UpdateProductCommandTestData
    {
        public static UpdateProductCommand ValidCommand =>
            new(Guid.NewGuid(), "PartNumber001", "Product1");

        public static UpdateProductCommand EmptyIdCommand =>
            new(Guid.Empty, "PartNumber001", "Product1");

        public static UpdateProductCommand EmptyNameCommand =>
            new(Guid.NewGuid(), "PartNumber001", "");

        public static UpdateProductCommand LongNameCommand =>
            new(Guid.NewGuid(), "PartNumber001", new string('A', 256));

        public static UpdateProductCommand EmptyPartnumberCommand =>
            new(Guid.NewGuid(), "", "Product1");

        public static UpdateProductCommand LongPartnumberCommand =>
            new(Guid.NewGuid(), new string('A', 256), "Product1");
    }
}
