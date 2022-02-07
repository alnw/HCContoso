using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GQL
{
    /// <summary>
    /// Represents the contract for a class that implements IGraphQLServerBuilder.
    /// </summary>
    public interface IGraphQLServerBuilder
    {
        /// <summary>Gets configuration</summary>
        IConfiguration Configuration { get; }

        /// <summary>Gets services</summary>
        IServiceCollection Services { get; }
    }
}
