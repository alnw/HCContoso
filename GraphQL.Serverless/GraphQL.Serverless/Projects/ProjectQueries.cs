using GQL.Data;
using GQL.DataLoaders;
using GQL.Database.Entities;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace GQL.Projects
{
    [ExtendObjectType("Query")]
    public class ProjectQueries
    {
        public async Task<ProjectData> GetProjectByIdAsync(
            int id,
            ProjectByIdDataLoader dataloader,
            CancellationToken cancellationToken)
        {
            return await dataloader.LoadAsync(id, cancellationToken);
        }
    }
}
