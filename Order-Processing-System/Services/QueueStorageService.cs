using Azure.Storage.Queues;

namespace Order_Processing_System.Services
{
    public class QueueStorageService
    {
        private readonly QueueServiceClient _queueServiceClient;
        private readonly QueueClient _queueClient;
        public QueueStorageService()
        {
            _queueServiceClient = new QueueServiceClient(
                "UseDevelopmentStorage=true"
                );
            _queueClient = _queueServiceClient.GetQueueClient("order-processing");
            _queueClient.CreateIfNotExists();
        }
        public async Task SendMessageAsync(string message)
        {
            await _queueClient.SendMessageAsync(message);
            Console.WriteLine("message successfully sent.");
        }
    }
}
