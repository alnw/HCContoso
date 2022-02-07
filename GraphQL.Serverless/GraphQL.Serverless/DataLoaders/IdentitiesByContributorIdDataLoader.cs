using GQL.Data;
using GQL.Database;
using GQL.Services.Mappers;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GQL.DataLoaders
{
    public class IdentitiesByContributorIdDataLoader : GroupedDataLoader<int, ContributorIdentityData>
    {
        /// <summary>Gets Database Context Factory</summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentitiesByContributorIdDataLoader"/> class.
        /// </summary>
        /// <param name="batchScheduler"><see cref="IBatchScheduler"/></param>
        /// <param name="dbContextFactory"><see cref="IDbContextFactory"/></param>
        public IdentitiesByContributorIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        protected override async Task<ILookup<int, ContributorIdentityData>> LoadGroupedBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            var results = new List<ContributorIdentityData>();

            await using ApplicationDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            await dbContext.ContributorIdentities
                                    .Where(x => keys.Contains(x.ContributorId))
                                    .ForEachAsync(identity =>
                                    {
                                        results.Add(DbContextMapper.MapDbEntity(identity));
                                    }, cancellationToken);
            

            return results.ToLookup(p => p.ContributorId);
        }
    }
}
