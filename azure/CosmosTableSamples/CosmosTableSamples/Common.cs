using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosTableSamples
{
    public class Common
    {
        ///This method will parse the connection string details 
        ///and validate that the account name and account key details 
        ///provided in the "Settings.json" file are valid.
        
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string connectionString)
        {
            CloudStorageAccount cloudStorageAccount;
            try
            {
                cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            }
            catch(FormatException)
            {
                Console.WriteLine("Invalid storage account information provided.");
                Console.ReadLine();
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided.");
                Console.ReadLine();
                throw;
            }

            return cloudStorageAccount;
        }

        ///The CloudTableClient class enables you to retrieve tables 
        ///and entities stored in Table storage. Because we don’t have any 
        ///tables in the Cosmos DB Table API account, let’s add the CreateTableAsync 
        ///method to the Common.cs class to create a table
        ///

        public static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;

            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            tableClient.TableClientConfiguration.UseRestExecutorForCosmosEndpoint = true; //this is to connect without 503 issue

            Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            if(await table.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Created Table named: {0}", tableName);
            }
            else
            {
                Console.WriteLine("Table named: {0} already exists.", tableName);
            }

            Console.WriteLine();
            return table;

        }
    }
}
