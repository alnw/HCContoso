using GQL.Data;
using GQL.DataLoaders;
using GQL.Database.Entities;
using HotChocolate.Types;
using System.Threading;
using System.Threading.Tasks;

namespace GQL.Teams
{
    [ExtendObjectType("Query")]
    public class TeamQueries
    {
        public async Task<TeamData> GetTeamByIdAsync(
            int id,
            TeamByIdDataLoader dataloader,
            CancellationToken cancellationToken)
        {
            return await dataloader.LoadAsync(id, cancellationToken);
        }
    }
}
