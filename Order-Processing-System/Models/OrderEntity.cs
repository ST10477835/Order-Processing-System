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
        public string Product { get; set; } = "";
        public int Quanitity { get; set; } = 0;
        public double Price { get; set; } = 0.0;
        public DateTime CreatedAt { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
