using Azure;
using Azure.Data.Tables;

namespace Order_Processing_System.Models
{
    public class OrderEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string CustomerName { get; set; } = "";
        public string Email { get; set; } = "";
        public int ProductId { get; set; } = 0;
        public int Quanitity { get; set; } = 0;
        public DateTime CreatedAt { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
