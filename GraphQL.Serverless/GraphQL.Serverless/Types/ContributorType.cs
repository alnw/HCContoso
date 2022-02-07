
using HotChocolate.Types;
using GQL.Data;
using GQL.DataLoaders;
using GQL.Contributors;

namespace GQL.Types
{
    public class ContributorType : ObjectType<ContributorData>
    {
        protected override void Configure(IObjectTypeDescriptor<ContributorData> descriptor)
        {
            descriptor
                .Field(t => t.Id)
                .Type<IntType>();

            descriptor
                .Field(t => t.Identities)
                .Type<ListType<NonNullType<ContributorIdentityType>>>()
                .ResolveWith<ContributorResolvers>(t => t.GetIdentitiesAsync(default!, default!, default))
                .Name("identities");
        }
    }
}