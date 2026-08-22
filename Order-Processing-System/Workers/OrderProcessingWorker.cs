using Order_Processing_System.Models;
using Order_Processing_System.Services;

namespace Order_Processing_System.Workers
{
    public class OrderProcessingWorker : BackgroundService
    {
        private readonly QueueStorageService _queueStorageService;
        public OrderProcessingWorker(QueueStorageService queueStorageService)
        {
            _queueStorageService = queueStorageService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var queueMessage = await _queueStorageService.ReceiveMessageAsync();

                if (queueMessage != null)
                {
                    Console.WriteLine("Worker received: {0}", queueMessage.MessageText);
                    await _queueStorageService.DeleteMessageAsync(queueMessage.MessageId, queueMessage.PopReceipt);
                    Console.WriteLine("Message Deleted successfully");
                }
            }
            await Task.Delay(5000, stoppingToken);
        }
    }
}
