
using HotChocolate;

namespace GQL.Data
{
    public class ContributorIdentityData
    {
        /// <summary>Gets or sets contributor id</summary>
        [GraphQLIgnore]
        public int ContributorId { get; set; }

        public int Id { get; set; }

        public string Identity { get; set; }
    }
}
