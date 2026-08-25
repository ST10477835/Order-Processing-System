using Azure.Data.Tables;
using Order_Processing_System.Models;

namespace Order_Processing_System.Services
{
    public class TableStorageService
    {
        private readonly TableServiceClient _tableServiceClient;
        public TableStorageService()
        {
            _tableServiceClient = new TableServiceClient(
                "UseDevelopmentStorage=true");
        }
        public TableClient GetOrderTable()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient("Orders");
            tableClient.CreateIfNotExists();
            return tableClient;
        }
        public TableClient GetProductTable()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient("Products");
            tableClient.CreateIfNotExists();
            return tableClient;
        }
        public async Task AddOrderAsync(OrderEntity order)
        {
            var tableClient = GetOrderTable();
            await tableClient.AddEntityAsync(order);
        }
        public List<Order> GetOrders()
        {
            var tableClient = GetOrderTable();
            var _orders = tableClient.Query<OrderEntity>();
            List<Order> orders = new List<Order>();
            foreach(OrderEntity order in _orders)
            {
                orders.Add(new Order
                {
                    OrderId = int.Parse(order.RowKey),
                    CustomerName = order.CustomerName,
                    Email = order.Email,
                    ProductId = order.ProductId,
                    Quanitity = order.Quanitity,
                    CreatedAt = order.CreatedAt
                });
            }
            return orders;
        }
        public List<Product> GetProducts()
        {
            var tableClient = GetProductTable();
            var _products = tableClient.Query<ProductEntity>();
            List<Product> products = new List<Product>();
            foreach(ProductEntity product in _products)
            {
                products.Add(new Product
                {
                    ProductId = int.Parse(product.RowKey),
                    Name = product.Name,
                    Price = product.Price
                });
            }
            return products;
        }
        public int CountOrders()
        {
            var tableClient = GetOrderTable();
            return tableClient.Query<OrderEntity>().Count();
        }
    }
}
