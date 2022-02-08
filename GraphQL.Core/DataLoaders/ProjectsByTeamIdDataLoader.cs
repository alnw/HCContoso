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
    public class ProjectsByTeamIdDataLoader : GroupedDataLoader<int, KeyValuePair<int, ProjectData>>
    {
        /// <summary>Gets Database Context Factory</summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsByTeamIdDataLoader"/> class.
        /// </summary>
        /// <param name="batchScheduler"><see cref="IBatchScheduler"/></param>
        /// <param name="dbContextFactory"><see cref="IDbContextFactory"/></param>
        public ProjectsByTeamIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory;
        }

        /// <inheritdoc/>
        protected override async Task<ILookup<int, KeyValuePair<int, ProjectData>>> LoadGroupedBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            var results = new Dictionary<int, List<ContributorData>>();

            await using ApplicationDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            var entities = await dbContext.ProjectStates.Include(x => x.Project)
                                    .Where(x => keys.Contains(x.Project.TeamId))
                                    .ToListAsync(cancellationToken);

            return entities.ToLookup(
                        p => p.Project.TeamId,
                        p => KeyValuePair.Create<int, ProjectData>(p.Project.TeamId, DbContextMapper.MapDbEntity(p.Project, p.OwnerId)));
        }
    }
}
