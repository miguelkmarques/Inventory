namespace InventoryVenturus.Domain
{
    public class ErrorLog
    {
        public Guid Id { get; set; }

        public Guid CorrelationId { get; set; }

        public string? RequestType { get; set; }

        public string? Request { get; set; }

        public string? Exception { get; set; }

        public DateTime Timestamp { get; set; }

        public ErrorLog() { }

        public ErrorLog(Guid correlationId, string? requestType, string? request, string? exception)
        {
            Id = Guid.NewGuid();
            CorrelationId = correlationId;
            RequestType = requestType;
            Request = request;
            Exception = exception;
            Timestamp = DateTime.UtcNow;
        }
    }
}
