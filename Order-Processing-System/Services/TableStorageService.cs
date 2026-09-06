using Azure;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        public Order GetOrder(int Id)
        {
            var orders = GetOrders();
            Order _order = new Order();
            foreach(Order order in orders)
            {
                if (order.OrderId == Id)
                {
                    _order = order;
                }
            }
            return _order;
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
        public async Task DeleteOrderAsync(int OrderId)
        {
            var tableClient = GetOrderTable();
            Console.WriteLine($"Inside DeleteOrderAsync. Order id = {OrderId}");
            await tableClient.DeleteEntityAsync("Orders", $"{OrderId}");
        }
        public int CountOrders()
        {
            var tableClient = GetOrderTable();
            return tableClient.Query<OrderEntity>().Count();
        }
        public int CountProducts()
        {
            var tableClient = GetProductTable();
            return tableClient.Query<ProductEntity>().Count();
        }
    }
}
