using System;
using System.Collections.Generic;

namespace GQL.Data
{
    public class TeamData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ContributorData> Members { get; set; }

        public IEnumerable<ProjectData> Projects { get; set; } = new List<ProjectData>();
    }
}
