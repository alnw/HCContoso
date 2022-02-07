// © Microsoft Corporation. All rights reserved.

using System;
using System.IO;
using System.Threading.Tasks;

using GQL.Extensions;

using HotChocolate.AzureFunctionsProxy;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;


namespace GQL
{
    /// <summary>
    /// For dotnet-isolated, Program is the entry point of function app
    /// </summary>
    public class Program
    {
        /// <summary>
        /// For dotnet-isolated, Program is the entry point of function app
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public static async Task Main()
        {
            var local_root = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot");
            var azure_root = $"{Environment.GetEnvironmentVariable("HOME")}/site/wwwroot";
            var visual_studio_root = Environment.CurrentDirectory;

            var actual_root = local_root ?? azure_root;

            if (!Directory.Exists(actual_root))
            {
                actual_root = visual_studio_root;
            }

            var config = GetConfiguration(actual_root);

            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddDemoGraphQLServer(config);

                    // Initialize Azure Functions Executor Proxy here...
                    services.AddAzureFunctionsGraphQL((options) =>
                    {
                        // The Path must match the exact routing path that the Azure Function HttpTrigger is bound to.
                        options.AzureFunctionsRoutePath = "/api/graphql/playground";
                    });
                })
                .Build();

            await host.RunAsync();
        }

        private static IConfiguration GetConfiguration(string appDirectory)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(appDirectory)
                .AddJsonFile($"app.settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return config;
        }
    }
}
