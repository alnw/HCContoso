
using HotChocolate.Types;
using GQL.Data;
using GQL.DataLoaders;
using GQL.Teams;

namespace GQL.Types
{
    public class TeamType : ObjectType<TeamData>
    {
        protected override void Configure(IObjectTypeDescriptor<TeamData> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<TeamByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(c => c.Members)
                .Type<ListType<NonNullType<ContributorType>>>()
                .ResolveWith<TeamResolvers>(t => t.GetMembersAsync(default!, default!, default))
                .Name("members");

            descriptor
                .Field(c => c.Projects)
                .Type<ListType<ProjectType>>()
                .ResolveWith<TeamResolvers>(t => t.GetProjectsAsync(default!, default!, default))
                .Name("projects");
        }
    }
} 