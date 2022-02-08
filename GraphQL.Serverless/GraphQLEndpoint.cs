// © Microsoft Corporation. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using HotChocolate.AzureFunctionsProxy;
using HotChocolate.AzureFunctionsProxy.IsolatedProcess;

using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;


namespace GQL
{
    /// <summary>
    /// Azure Function Endpoint for the GraphQL Schema queries
    /// </summary>
    public class GraphQLEndpoint
    {
        private readonly IGraphQLAzureFunctionsExecutorProxy _graphqlExecutorProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphQLEndpoint"/> class.
        /// </summary>
        /// <param name="graphQLProxy">GraphQL Proxy for Azure Function to Executor queries.</param>
        /// <param name="httpAccessor">http accessor to access http context.</param>
        public GraphQLEndpoint(IGraphQLAzureFunctionsExecutorProxy graphQLProxy, IHttpContextAccessor httpAccessor)
        {
            _graphqlExecutorProxy = graphQLProxy;
        }

        /// <summary>
        /// Entry point for the GraphQLEndpoint for GraphQL queries.
        /// </summary>
        /// <param name="req">The http request.</param>
        /// <param name="executionContext">The ILogger instance for this Azure Function.</param>
        /// <returns><see cref="Task"/>.</returns>
        [Function(nameof(GraphQLEndpoint))]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "graphql")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger<GraphQLEndpoint>();

            var response = await _graphqlExecutorProxy.ExecuteFunctionsQueryAsync(req, logger).ConfigureAwait(false);
            return response;
        }
    }
}
