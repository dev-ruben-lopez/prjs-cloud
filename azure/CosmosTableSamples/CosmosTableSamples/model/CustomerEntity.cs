using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableSamples.model
{
    class CustomerEntity : TableEntity
    {
        public CustomerEntity()
        {

        }

        public CustomerEntity (string lastName, string firstName)
        {
            PartitionKey = lastName;
            RowKey = firstName;
        }

        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}
