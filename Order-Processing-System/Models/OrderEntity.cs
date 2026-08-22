using Azure;
using Azure.Data.Tables;

namespace Order_Processing_System.Models
{
    public class OrderEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public int UserId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string Status { get; set; } = "";
        public DateTime? CreatedAt { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
