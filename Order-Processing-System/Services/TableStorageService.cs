using Azure.Data.Tables;

namespace Order_Processing_System.Services
{
    public class TableStorageService
    {
        private readonly TableServiceClient _tableServiceClient;
        private readonly TableClient _tableClient;

        public TableStorageService()
        {
            _tableServiceClient = new TableServiceClient(
                "UseDevelopmentStorage=true");

            _tableClient = _tableServiceClient.GetTableClient("Orders");
            _tableClient.CreateIfNotExists();
        }
    }
}
