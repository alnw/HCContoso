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
    public class ContributorByIdDataLoader : BatchDataLoader<int, ContributorData>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ContributorByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, ContributorData>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            var results = new List<ContributorData>();
            await dbContext.Contributors
                    .Where(s => keys.Contains(s.Id))
                    .ForEachAsync((p) => results.Add(DbContextMapper.MapDbEntity(p)),
                cancellationToken);

            return results.ToDictionary(t => t.Id);
        }
    }
}