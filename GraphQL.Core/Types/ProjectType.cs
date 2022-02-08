
using HotChocolate.Types;
using GQL.Data;
using GQL.DataLoaders;
using GQL.Projects;

namespace GQL.Types
{
    public class ProjectType : ObjectType<ProjectData>
    {
        protected override void Configure(IObjectTypeDescriptor<ProjectData> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<ProjectByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(c => c.Owner)
                .Type<NonNullType<ContributorType>>()
                .ResolveWith<ProjectResolvers>(t => t.GetOwnerAsync(default!, default!, default!));
        }

    }
}