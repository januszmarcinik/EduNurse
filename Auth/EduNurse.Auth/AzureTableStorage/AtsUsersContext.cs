using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Auth.AzureTableStorage
{
    internal class AtsUsersContext
    {
        public const string UsersPartitionKey = "User";

        public CloudTable Table { get; }

        public AtsUsersContext(Settings settings)
        {
            var storageAccount = CloudStorageAccount.Parse(settings.AzureTableStorage);
            var tableClient = storageAccount.CreateCloudTableClient();
            Table = tableClient.GetTableReference(settings.AuthTableName);

            Table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }

        public async Task<T> GetSingleAsync<T>(TableQuery<T> query) where T : TableEntity, new()
        {
            var segment = await Table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());
            return segment.SingleOrDefault();
        }
    }
}
