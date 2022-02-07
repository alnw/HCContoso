using HotChocolate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GQL.Data
{
    public class ProjectData
    {
        public int Id { get; set; }

        [GraphQLIgnore]
        public int OwnerId { get; set; }

        [GraphQLIgnore]
        public int TeamId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ContributorData Owner { get; set; }
    }
}
