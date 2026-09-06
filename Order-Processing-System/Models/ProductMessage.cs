namespace Order_Processing_System.Models
{
    public class ProductMessage
    {
        public string Operation { get; set; } = "";
        public Product Product { get; set; } = new Product();
    }
}
