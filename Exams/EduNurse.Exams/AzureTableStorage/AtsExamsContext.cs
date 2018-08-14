using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace EduNurse.Exams.AzureTableStorage
{
    internal class AtsExamsContext
    {
        public const string ExamsPartitionKey = "Exam";
        public const string QuestionsPartitionKey = "Question";

        public CloudTable Table { get; }

        public AtsExamsContext(ExamsSettings examsSettings)
        {
            var storageAccount = CloudStorageAccount.Parse(examsSettings.AzureTableStorage);
            var tableClient = storageAccount.CreateCloudTableClient();
            Table = tableClient.GetTableReference(examsSettings.ExamsTableName);

            Table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }

        public async Task<T> GetSingleAsync<T>(TableQuery<T> query) where T : TableEntity, new()
        {
            var segment = await Table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());
            return segment.SingleOrDefault();
        }

        public async Task<List<T>> GetListAsync<T>(TableQuery<T> query) where T : TableEntity, new()
        {
            var segment = await Table.ExecuteQuerySegmentedAsync(query, new TableContinuationToken());
            return segment.Results;
        }

        public async Task DeleteTableIfExistsAsync()
        {
            await Table.DeleteIfExistsAsync();
        }
    }
}
