using GQL.Data;
using GQL.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GQL.Services.Mappers
{
    public static class DbContextMapper
    {
        internal static ContributorData MapDbEntity(Contributor contributor)
        {
            return contributor == null ? null : new ContributorData()
            {
                Id = contributor.Id,
                FirstName = contributor.FirstName,
                LastName = contributor.LastName,
                DisplayName = $"{contributor.FirstName} {contributor.LastName}",
                Email = contributor.Email,
            };
        }

        internal static ContributorIdentityData MapDbEntity(ContributorIdentity identity)
        {
            return identity == null ? null : new ContributorIdentityData()
            {
                ContributorId = identity.ContributorId,
                Identity = identity.Identity,
            };
        }

        internal static ProjectData MapDbEntity(Project project, int ownerId = 0)
        {
            return project == null ? null : new ProjectData()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                TeamId = project.TeamId,
                OwnerId = ownerId,
            };
        }

        internal static TeamData MapDbEntity(Team team)
        {
            return team == null ? null : new TeamData()
            {
                Id = team.Id,
                Name = team.Name,
            };
        }
    }
}
