// © Microsoft Corporation. All rights reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GQL.Database
{
    /// <summary>
    /// A factory for creating derived <see cref="ApplicationDbContext"/> instances.
    /// </summary>
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private const string MigrationAssemblyName = "GQL.Database.Migrations";
        private const string DatabaseName = "CompanyDB";

        /// <inheritdoc/>
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer($@"Server=(localdb)\mssqllocaldb;Database={DatabaseName}", b =>
            {
                b.EnableRetryOnFailure();
                b.MigrationsAssembly(MigrationAssemblyName);
            });

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
