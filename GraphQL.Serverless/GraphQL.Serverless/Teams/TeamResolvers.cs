using GQL.Data;
using GQL.DataLoaders;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GQL.Teams
{
    public class TeamResolvers
    {
        public async Task<List<ContributorData>> GetMembersAsync(
            [Parent] TeamData team,
            ContributorsByTeamIdDataLoader dataloader,
            CancellationToken cancellationToken)
        {
            var results = await dataloader.LoadAsync(team.Id, cancellationToken);
            return results.Where(x => x.Key == team.Id).Select(x => x.Value).ToList();
        }

        public async Task<List<ProjectData>> GetProjectsAsync(
            [Parent] TeamData team,
            ProjectsByTeamIdDataLoader dataloader,
            CancellationToken cancellationToken)
        {
            var results = await dataloader.LoadAsync(team.Id, cancellationToken);
            return results?.Length == 0 ? null : results.Where(x => x.Key == team.Id).Select(x => x.Value).ToList();
        }
    }
}
