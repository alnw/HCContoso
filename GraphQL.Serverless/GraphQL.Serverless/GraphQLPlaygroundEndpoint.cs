using System;

using System.Security.Claims;

using System.Threading;
using System.Threading.Tasks;


using HotChocolate.AzureFunctionsProxy;
using HotChocolate.AzureFunctionsProxy.IsolatedProcess;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace GQL
{
    /// <summary>
    /// Azure Function Endpoint for the GraphQL Schema Explorer
    /// </summary>
    public class GraphQLPlaygroundEndpoint
    {
        private readonly IGraphQLAzureFunctionsExecutorProxy _graphQLExecutorProxy;

        public GraphQLPlaygroundEndpoint(
            IGraphQLAzureFunctionsExecutorProxy graphQLProxy)
        {
            _graphQLExecutorProxy = graphQLProxy;
        }

        /// <summary>
        /// Entry point for the GraphQLPlaygroundEndpoint to explore GraphQL schema.
        /// </summary>
        /// <param name="req">The http request.</param>
        /// <param name="executionContext">The ILogger instance for this Azure Function.</param>
        /// <returns><see cref="Task"/></returns>
        [Function(nameof(GraphQLPlaygroundEndpoint))]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "graphql/playground/{*path}")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger<GraphQLPlaygroundEndpoint>();
            logger.LogInformation("C# GraphQL Request processing via Serverless AzureFunctions...");

            // Get principle from HttpRequestData
            var principal = new ClaimsPrincipal(req.Identities);

            // SECURE this endpoint against actual Data Queries
            //  This is useful for exposing the playground anonymously, but keeping the actual GraphQL data endpoint
            //  secured with AzureFunction token security and/or other authorization approach.
            if (HttpMethods.IsPost(req.Method) || (HttpMethods.IsGet(req.Method) && !string.IsNullOrWhiteSpace(req.GetQueryStringParam("query"))))
            {
                return req.CreateBadRequestErrorMessageResponse("POST or GET GraphQL queries are invalid for the Playground endpoint.");
            }

            return await _graphQLExecutorProxy.ExecuteFunctionsQueryAsync(
                req,
                logger,
                CancellationToken.None)
            .ConfigureAwait(false);
        }
    }
}
