using Order_Processing_System.Models;
using Order_Processing_System.Services;
using System.Text.Json;

namespace Order_Processing_System.Workers
{
    public class OrderProcessingWorker : BackgroundService
    {
        private readonly QueueStorageService _queueStorageService;
        private readonly BlobStorageService _blobStorageService;
        private readonly TableStorageService _tableStorageService;
        public OrderProcessingWorker(
            QueueStorageService queueStorageService,
            BlobStorageService blobStorageService,
            TableStorageService tableStorageService)
        {
            _queueStorageService = queueStorageService;
            _blobStorageService = blobStorageService;
            _tableStorageService = tableStorageService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var queueMessage = await _queueStorageService.ReceiveMessageAsync();

                if (queueMessage != null)
                {
                    OrderMessage? orderMessage = JsonSerializer.Deserialize<OrderMessage>(queueMessage.MessageText);
                    if(orderMessage != null)
                    {
                        switch (orderMessage.Operation)
                        {
                            case "Create Order":
                                Order? order = orderMessage.Order;
                                var orderEntity = new OrderEntity
                                    {
                                        PartitionKey = "Orders",
                                        RowKey = order.OrderId.ToString(),
                                        CustomerName = order.CustomerName,
                                        Email = order.Email,
                                        ProductId = order.ProductId,
                                        Quanitity = order.Quanitity,
                                        CreatedAt = order.CreatedAt
                                    };
                                    try
                                    {
                                        await _tableStorageService.AddOrderAsync(orderEntity);
                                        Console.WriteLine("Table entry was created.");
                                        await _blobStorageService.CreateBlobAsync(order);
                                        Console.WriteLine("Blob entry was created.");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"Failed to process order: {ex.Message}");
                                    }
                                break;
                            case "Delete Order":
                                    int orderId = orderMessage.OrderId;
                                Console.WriteLine(queueMessage.MessageText);
                                Console.WriteLine(orderMessage);
                                Console.WriteLine($"Order Id is {orderId}");
                                    await _tableStorageService.DeleteOrderAsync(orderId);
                                await _blobStorageService.DeleteBlobAsync(orderId);
                                    Console.WriteLine($"Successfully Deleted Record {orderId}");
                                break;
                            case "Create Product":
                                break;
                        }

                    }
                    Console.WriteLine("Worker received: {0}", queueMessage.MessageText);
                    await _queueStorageService.DeleteMessageAsync(queueMessage.MessageId, queueMessage.PopReceipt);
                    Console.WriteLine("Message Deleted successfully");
                }
            }
            await Task.Delay(5000, stoppingToken);
        }
    }
}
