using System.Linq;
using System.Threading.Tasks;
using EduNurse.Auth.Settings;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Auth.AzureTableStorage
{
    internal class AtsUsersContext
    {
        public const string UsersPartitionKey = "User";

        public CloudTable Table { get; }

        public AtsUsersContext(AuthSettings authSettings)
        {
            var storageAccount = CloudStorageAccount.Parse(authSettings.AzureTableStorage);
            var tableClient = storageAccount.CreateCloudTableClient();
            Table = tableClient.GetTableReference(authSettings.AuthTableName);

            Table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }

        public async Task<T> GetSingleAsync<T>(TableQuery<T> query) where T : TableEntity, new()
        {
            var segment = await Table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());
            return segment.SingleOrDefault();
        }

        public async Task DeleteTableIfExistsAsync()
        {
            await Table.DeleteIfExistsAsync();
        }
    }
}
