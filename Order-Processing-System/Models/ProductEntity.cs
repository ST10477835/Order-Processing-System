using Azure;
using Azure.Data.Tables;

namespace Order_Processing_System.Models
{
    public class ProductEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; } = 0.0;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
