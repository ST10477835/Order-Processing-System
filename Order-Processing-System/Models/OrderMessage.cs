namespace Order_Processing_System.Models
{
    public class OrderMessage
    {
        public string Operation { get; set; } = "";
        public Order? Order { get; set; } = new Order();

        public int OrderId { get; set; } = 0;
    }
}
