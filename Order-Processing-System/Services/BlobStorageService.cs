using Azure.Storage.Blobs;

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

            _blobContainerClient = _blobServiceClient.GetBlobContainerClient("Orders");
            _blobContainerClient.CreateIfNotExists();
        }
    }
}
