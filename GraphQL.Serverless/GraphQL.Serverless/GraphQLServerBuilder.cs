
using System.Runtime.CompilerServices;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GQL
{
    /// <summary>
    /// Builder specific for GraphQL Server that implements IGraphQLServerBuilder.
    /// </summary>
    public class GraphQLServerBuilder : IGraphQLServerBuilder
    {
        public GraphQLServerBuilder(IConfiguration configuration, IServiceCollection services)
        {
            Configuration = configuration;
            Services = services;
        }

        /// <summary>Gets the current configuration.</summary>
        public IConfiguration Configuration { get; }

        /// <summary>Gets the current services.</summary>
        public IServiceCollection Services { get; }
    }
}
