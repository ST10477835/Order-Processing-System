using System.Data;

namespace Order_Processing_System.Models
{
    public class Order
    {
        public int OrderId { get; set; } = 0;
        public string CustomerName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Product { get; set; } = "";
        public int Quanitity { get; set; } = 0;
        public double Price { get; set; } = 0.0;
        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get => _createdAt;
            set => _createdAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}
