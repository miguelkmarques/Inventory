namespace InventoryVenturus.Domain
{
    public class Stock
    {
        public Guid StockId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Stock() { }

        public Stock(Guid productId, int quantity)
        {
            StockId = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
