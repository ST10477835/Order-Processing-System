using Order_Processing_System.Models;
using Order_Processing_System.Services;
using System.Text.Json;

namespace Order_Processing_System.Workers
{
    public class OrderProcessingWorker : BackgroundService
    {
        private readonly QueueStorageService _queueStorageService;
        private readonly BlobStorageService _blobStorageService;
        public OrderProcessingWorker(
            QueueStorageService queueStorageService,
            BlobStorageService blobStorageService)
        {
            _queueStorageService = queueStorageService;
            _blobStorageService = blobStorageService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var queueMessage = await _queueStorageService.ReceiveMessageAsync();

                if (queueMessage != null)
                {
                    Order? order = JsonSerializer.Deserialize<Order>(queueMessage.MessageText);
                    if(order != null)
                    {
                        await _blobStorageService.CreateBlobAsync(order);
                        Console.WriteLine("Blob was created.");
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
