using GQL.Data;
using GQL.DataLoaders;
using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GQL.Projects
{
    public class ProjectResolvers
    {
        public async Task<ContributorData> GetOwnerAsync(
        [Parent] ProjectData project,
        ContributorByIdDataLoader dataLoader,
        CancellationToken cancellationToken)
        {
            return await dataLoader.LoadAsync(project.OwnerId, cancellationToken);
        }
    }
}
