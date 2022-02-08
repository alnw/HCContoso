using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GreenDonut;
using GQL.Data;
using GQL.Database;
using GQL.Services.Mappers;

namespace GQL.DataLoaders
{
    public class TeamByIdDataLoader : BatchDataLoader<int, TeamData>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public TeamByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ?? 
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, TeamData>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext = 
                _dbContextFactory.CreateDbContext();

            var results = new List<TeamData>();
            await dbContext.Teams
                .Where(s => keys.Contains(s.Id))
                .ForEachAsync(t => results.Add(DbContextMapper.MapDbEntity(t)),
                cancellationToken);

            return results.ToDictionary(t => t.Id);
        }
    }
}