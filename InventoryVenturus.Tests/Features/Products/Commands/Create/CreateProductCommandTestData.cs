using InventoryVenturus.Features.Products.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Commands.Create
{
    public class CreateProductCommandTestData
    {
        public static CreateProductCommand ValidCommand =>
            new ("PartNumber001", "Product1");

        public static CreateProductCommand EmptyNameCommand =>
            new ("PartNumber001", "");

        public static CreateProductCommand LongNameCommand =>
            new ("PartNumber001", new string('A', 256));

        public static CreateProductCommand EmptyPartnumberCommand =>
            new ("", "Product1");

        public static CreateProductCommand LongPartnumberCommand =>
            new(new string('A', 256), "Product1");
    }
}
