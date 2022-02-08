using GQL.Data;
using HotChocolate.Types;


namespace GQL.Types
{
    public class ContributorIdentityType : ObjectType<ContributorIdentityData>
    {
        /// <inheritdoc/>
        protected override void Configure(IObjectTypeDescriptor<ContributorIdentityData> descriptor)
        {
            descriptor
                .Field(t => t.Id)
                .Type<IntType>();

            descriptor
                .Field(t => t.Identity)
                .Type<StringType>();
        }
    }
}
