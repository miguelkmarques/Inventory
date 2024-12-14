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
            new ("ValidPartnumber", "ValidName");

        public static CreateProductCommand EmptyNameCommand =>
            new ("ValidPartnumber", "");

        public static CreateProductCommand LongNameCommand =>
            new ("ValidPartnumber", new string('A', 256));

        public static CreateProductCommand EmptyPartnumberCommand =>
            new ("", "ValidName");

        public static CreateProductCommand LongPartnumberCommand =>
            new(new string('A', 256), "ValidName");
    }
}
