using System;
using System.Collections.Generic;
using System.Text;

namespace CosmosTableSamples
{

    using Microsoft.Extensions.Configuration;
    class AppSettings
    {
        public string StorageConnectionString { get; set;}

        public static AppSettings LoadAppSettings()
        {

            IConfiguration configRoot = new ConfigurationBuilder()
                .AddJsonFile("Settings.json")
                .Build();

            AppSettings appSettings = configRoot.Get<AppSettings>();
            return appSettings;

        }

    }
}
