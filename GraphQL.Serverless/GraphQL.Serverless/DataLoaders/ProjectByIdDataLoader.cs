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
    public class ProjectByIdDataLoader : BatchDataLoader<int, ProjectData>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public ProjectByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ?? 
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, ProjectData>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext = 
                _dbContextFactory.CreateDbContext();

            var results = new List<ProjectData>();
            await dbContext.ProjectStates.Include(x => x.Project)
                .Where(s => keys.Contains(s.Id))
                .ForEachAsync(s =>
                {
                    var project = DbContextMapper.MapDbEntity(s.Project);
                    project.OwnerId = s.OwnerId;
                    results.Add(project);
                }, cancellationToken);

            return results.ToDictionary(t => t.Id);
        }
    }
}