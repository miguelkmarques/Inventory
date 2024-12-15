using InventoryVenturus.Features.Products.Commands.Delete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryVenturus.Tests.Features.Products.Commands.Delete
{
    public class DeleteProductCommandTestData
    {
        public static DeleteProductCommand ValidCommand =>
            new(Guid.NewGuid());
    }
}
