using GQL.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GQL.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Contributor>()
                .HasIndex(a => a.Email)
                .IsUnique();
        }

        public DbSet<Contributor> Contributors { get; set; } = default!;

        public DbSet<Project> Projects { get; set; } = default!;

        public DbSet<ProjectState> ProjectStates { get; set; } = default!;

        public DbSet<Team> Teams { get; set; } = default!;

        public DbSet<TeamRole> TeamRoles { get; set; } = default!;

        public DbSet<ContributorIdentity> ContributorIdentities { get; set; } = default!;
    }
}