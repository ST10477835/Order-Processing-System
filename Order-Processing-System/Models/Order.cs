namespace Order_Processing_System.Models
{
    public class Order
    {
        public int OrderId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string Status { get; set; } = "";
        public DateTime? CreatedAt { get; set; } 
    }
}
