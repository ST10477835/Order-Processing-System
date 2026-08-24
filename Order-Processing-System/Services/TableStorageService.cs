using Azure.Data.Tables;
using Order_Processing_System.Models;

namespace Order_Processing_System.Services
{
    public class TableStorageService
    {
        private readonly TableServiceClient _tableServiceClient;
        private readonly TableClient _tableClient;

        public TableStorageService()
        {
            _tableServiceClient = new TableServiceClient(
                "UseDevelopmentStorage=true");

            _tableClient = _tableServiceClient.GetTableClient("Orders");
            _tableClient.CreateIfNotExists();
        }
        public async Task AddOrderAsync(OrderEntity order)
        {
            await _tableClient.AddEntityAsync(order);
        }
        public List<Order>  GetOrders()
        {
            var _orders = _tableClient.Query<OrderEntity>();
            List<Order> orders = new List<Order>();
            foreach(OrderEntity order in _orders)
            {
                orders.Add(new Order
                {
                    OrderId = int.Parse(order.RowKey),
                    CustomerName = order.CustomerName,
                    Email = order.Email,
                    Product = order.Product,
                    Quanitity = order.Quanitity,
                    Price = order.Price,
                    CreatedAt = order.CreatedAt
                });
            }
            return orders;
        }
        public int Count()
        {
            return _tableClient.Query<OrderEntity>().Count();
        }
    }
}
