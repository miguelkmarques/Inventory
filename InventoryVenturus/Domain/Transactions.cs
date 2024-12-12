namespace InventoryVenturus.Domain
{
    public class Transactions
    {
        public Guid TransactionId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } = default!;
        public decimal Cost { get; set; }

        public Transactions() { }

        public Transactions(Guid productId, int quantity, string transactionType, DateTime transactionDate, decimal cost)
        {
            TransactionId = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            TransactionType = transactionType;
            TransactionDate = transactionDate;
            Cost = cost;
        }
    }
}
