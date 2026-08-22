using Azure.Storage.Queues;
using Order_Processing_System.Models;

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
        public async Task<QueueMessageResult?> ReceiveMessageAsync()
        {
            var response = await _queueClient.ReceiveMessageAsync();
            if (response.Value == null)
            {
                return null;
            }
            return new QueueMessageResult
            {
                MessageText = response.Value.MessageText,
                MessageId = response.Value.MessageId,
                PopReceipt = response.Value.PopReceipt
            };
        }
        public async Task DeleteMessageAsync(string messageId, string popReceipt)
        {
            await _queueClient.DeleteMessageAsync(messageId, popReceipt);
        }
    }
}
