using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GQL.DataLoaders;
using GQL.Database;
using GQL.Projects;
using GQL.Types;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GQL.Teams;

namespace GQL.Extensions
{
    /// <summary>
    /// Service collection extension for GraphQL Server
    /// </summary>
    public static class GraphQLServiceCollection
    {
        public static IGraphQLServerBuilder AddDemoGraphQLServer(
                this IServiceCollection services,
                IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentException($"configuration is missing");
            }

            var builder = new GraphQLServerBuilder(configuration, services);
            services.AddPooledDbContextFactory<ApplicationDbContext>(
                (s, b) =>
                {
                    var connection = new SqlConnection
                    {
                        ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=CompanyDB",
                    };

                    b.UseSqlServer(connection, options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), new List<int>()))
                   .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>());
                });
            services.AddHttpContextAccessor();

            var gqlBuilder = builder.Services.AddGraphQLServer();

            gqlBuilder.AddQueryType(d => d.Name("Query"))
                      .AddTypeExtension<ProjectQueries>()
                      .AddTypeExtension<TeamQueries>()

                    // Types
                    .AddType<ContributorType>()
                    .AddType<ContributorIdentityType>()
                    .AddType<ProjectType>()
                    .AddType<TeamType>()

                    // DataLoaders
                    .AddDataLoader<ContributorByIdDataLoader>()
                    .AddDataLoader<ContributorsByTeamIdDataLoader>()
                    .AddDataLoader<IdentitiesByContributorIdDataLoader>()
                    .AddDataLoader<ProjectByIdDataLoader>()
                    .AddDataLoader<ProjectsByTeamIdDataLoader>()
                    .AddDataLoader<TeamByIdDataLoader>()

                    // Add Operation
                    .AddFiltering()

                    // Register DB Context, Let HC to handle service scopes
                    .RegisterDbContext<ApplicationDbContext>(kind: HotChocolate.Data.DbContextKind.Pooled);

            return builder;
        }
    }
}
