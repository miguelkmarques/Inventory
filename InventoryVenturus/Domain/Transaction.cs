namespace InventoryVenturus.Domain
{
    public enum TransactionType
    {
        Addition = 0,
        Consumption = 1
    }

    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime TransactionDate { get; set; }

        public TransactionType TransactionType { get; set; } = default!;

        public decimal Cost { get; set; }

        public Transaction() { }

        public Transaction(Guid productId, int quantity, TransactionType transactionType, DateTime transactionDate, decimal cost)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            TransactionType = transactionType;
            TransactionDate = transactionDate;
            Cost = cost;
        }
    }
}
