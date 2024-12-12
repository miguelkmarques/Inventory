using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryVenturus.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Partnumber { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }

        public Product() { }
        public Product(string name, string partnumber, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Partnumber = partnumber;
            Price = price;
        }
    }
}
