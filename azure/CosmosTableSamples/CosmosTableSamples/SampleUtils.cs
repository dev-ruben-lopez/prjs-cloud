using CosmosTableSamples.model;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CosmosTableSamples
{
    class SampleUtils
    {

        /// <summary>
        /// Insert or Merge
        /// </summary>
        /// <param name="table"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<CustomerEntity> InsertOfMergeEntityAsync(CloudTable table, CustomerEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                CustomerEntity insertedCustomer = result.Result as CustomerEntity;


                //get the request units consumed by the current operation
                if(result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedCustomer;

            }
            catch (StorageException se)
            {
                Console.WriteLine(se.Message);
                Console.ReadLine();
                throw;
            }



        }


        /// <summary>
        /// Get entity from a partition by using the Retrieve method under the T
        /// ableOperation class. The following code example gets the partition key row key, 
        /// email and phone number of a customer entity
        /// </summary>
        /// <param name="table"></param>
        /// <param name="partitionKey"></param>
        /// <param name="rowKey"></param>
        /// <returns></returns>
        
        public static async Task<CustomerEntity> RetrieveEntityUsingPointQueryAsync(CloudTable table, string partitionKey, string rowKey)
        {
            try
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>(partitionKey, rowKey);
                TableResult result = await table.ExecuteAsync(retrieveOperation);
                CustomerEntity customer = result.Result as CustomerEntity;


                if (customer != null)
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}", customer.PartitionKey, customer.RowKey, customer.email, customer.phoneNumber);
                }


                // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure CosmoS DB 
                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of Retrieve Operation: " + result.RequestCharge);
                }

                return customer;

            }

            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }


        }


        /// <summary>
        /// Delete enttiy
        /// </summary>
        /// <param name="table"></param>
        /// <param name="entityToDelete"></param>
        /// <returns></returns>
        public static async Task DeleteEntityAsync(CloudTable table, CustomerEntity entityToDelete)
        {

            try
            {

                if(entityToDelete == null)
                {
                    Console.WriteLine("No entity to delete provided");
                    throw new ArgumentNullException("deleteEntity");
                }


                TableOperation deleteOperation = TableOperation.Delete(entityToDelete);
                TableResult result = await table.ExecuteAsync(deleteOperation);

                // Get the request units consumed by the current operation. RequestCharge of a TableResult is only applied to Azure CosmoS DB 

                if(result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of Delete Operation: " + result.RequestCharge);
                }
            }
            catch(StorageException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                throw;
            }


        }





    }
}
