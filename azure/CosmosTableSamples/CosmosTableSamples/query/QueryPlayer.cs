using CosmosTableSamples.model;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosTableSamples.query
{
    class QueryPlayer
    {
        /// <summary>
        /// Get entity from a partition by using the TableQuery and EntityResolver
        /// </summary>
        /// <param name="table"></param>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>

        public static async Task RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey)
        {
            try
            {
                string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;

                CloudStorageAccount XYZ_storage = CloudStorageAccount.Parse(storageConnectionString);
                CloudTableClient XYZ_table_client = XYZ_storage.CreateCloudTableClient();
                CloudTable XYZ_table = XYZ_table_client.GetTableReference("Player");

                TableQuery<DynamicTableEntity> query = new TableQuery<DynamicTableEntity>()
                    .Select(new string[] { "score" }).Where(TableQuery
                    .GenerateFilterConditionForInt("score", QueryComparisons.GreaterThanOrEqual, 4000)).Take(10);
                
                EntityResolver<KeyValuePair<string, int?>> resolver = (partitionKey, rowKey, ts, props, etag)
                        => new KeyValuePair<string, int?>(rowKey, props["score"].Int32Value);

                foreach(var scoreItem in XYZ_table.ExecuteQuery(query,resolver,null,null))
                {
                    Console.WriteLine(scoreItem.Key);
                    Console.WriteLine(scoreItem.Value);
                }


            }

            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }


        }
    }
}
