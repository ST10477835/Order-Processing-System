namespace Order_Processing_System.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
