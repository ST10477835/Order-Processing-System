using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http.HttpResults;
using Order_Processing_System.Models;
using System.Text;
using System.Text.Json;

namespace Order_Processing_System.Services
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _blobContainerClient;
        private readonly string connectionString = "UseDevelopmentStorage=true";

        public BlobStorageService()
        {
            _blobServiceClient = new BlobServiceClient(
                connectionString);

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
        public async Task DeleteBlobAsync(int OrderId)
        {
            BlobClient blobClient = new BlobClient(connectionString, "orders", $"order-{OrderId}");
            await blobClient.DeleteIfExistsAsync();
            Console.WriteLine("Blob successfully deleted");
        }
        public async Task UpdateBlobAsync(Order order)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient($"order-{order.OrderId}");
            string json = JsonSerializer.Serialize(order);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            using MemoryStream stream = new MemoryStream(byteArray);

            // Overwrite the existing blob
            await blobClient.UploadAsync(stream, overwrite: true);
        }
    }
    }

