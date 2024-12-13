namespace InventoryVenturus.Domain
{
    public class Stock
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Stock() { }

        public Stock(Guid productId, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
