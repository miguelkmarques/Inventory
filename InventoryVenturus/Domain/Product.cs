using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryVenturus.Domain
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Partnumber { get; set; } = default!;

        public string Name { get; set; } = default!;

        public decimal Price { get; set; } = 0;

        public Product() { }
        public Product(string name, string partnumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            Partnumber = partnumber;
        }
    }
}
