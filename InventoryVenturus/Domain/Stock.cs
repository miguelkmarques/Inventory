namespace InventoryVenturus.Domain
{
    public class Stock
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public Stock() { }

        public Stock(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
