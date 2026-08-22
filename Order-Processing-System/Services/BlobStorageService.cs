using Azure.Storage.Blobs;
using Order_Processing_System.Models;
using System.Text;
using System.Text.Json;

namespace Order_Processing_System.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorageService()
        {
            _blobServiceClient = new BlobServiceClient(
                "UseDevelopmentStorage=true");

            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("orders");
            _blobContainerClient.CreateIfNotExists();
        }

        public async Task CreateBlobAsync(Order order)
        {
            BlobClient blob = _blobContainerClient.GetBlobClient($"order-{order.OrderId}");
            string json = JsonSerializer.Serialize(order);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            using MemoryStream stream = new MemoryStream(byteArray);

            await blob.UploadAsync(stream, overwrite: true);
        }
    }
}
