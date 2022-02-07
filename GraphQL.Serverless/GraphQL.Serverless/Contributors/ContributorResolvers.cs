using GQL.Data;
using GQL.DataLoaders;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GQL.Contributors
{
    public class ContributorResolvers
    {
        public async Task<IEnumerable<ContributorIdentityData>> GetIdentitiesAsync(
                [Parent] ContributorData contributor,
                IdentitiesByContributorIdDataLoader groupDataLoader,
                CancellationToken cancellationToken)
        {
                var results = await groupDataLoader.LoadAsync(contributor.Id, cancellationToken);
                return results?.Length > 0 ? results : null;
        }
    }
}
