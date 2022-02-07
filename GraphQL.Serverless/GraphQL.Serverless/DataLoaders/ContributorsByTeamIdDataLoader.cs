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
    public class ContributorsByTeamIdDataLoader : GroupedDataLoader<int, KeyValuePair<int, ContributorData>>
    {
        /// <summary>Gets Database Context Factory</summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContributorsByTeamIdDataLoader"/> class.
        /// </summary>
        /// <param name="batchScheduler"><see cref="IBatchScheduler"/></param>
        /// <param name="dbContextFactory"><see cref="IDbContextFactory"/></param>
        public ContributorsByTeamIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        protected override async Task<ILookup<int, KeyValuePair<int, ContributorData>>> LoadGroupedBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            var results = new Dictionary<int, List<ContributorData>>();

            await using ApplicationDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            var entities = await dbContext.TeamRoles.Where(x => keys.Contains(x.TeamId))
                                    .Include(x => x.Member).ToListAsync(cancellationToken);

            return entities.ToLookup(
                        p => p.TeamId,
                        p => KeyValuePair.Create<int, ContributorData>(p.TeamId, DbContextMapper.MapDbEntity(p.Member)));
        }
    }
}
