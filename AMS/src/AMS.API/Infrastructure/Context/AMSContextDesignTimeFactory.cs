using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using AMS.Infrastructure.Context;

namespace AMS.API.Infrastructure.Context
{
    public class AMSContextDesignTimeFactory : IDesignTimeDbContextFactory<AMSContext>
    {
        public AMSContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AMSContext>();

            optionsBuilder.UseSqlServer(GetConnectionString(), options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

            return new AMSContext(optionsBuilder.Options);
        }

        private static string GetConnectionString()
        {
            var configurationBuilder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile($"{Directory.GetCurrentDirectory()}/bin/Debug/net8.0/appsettings.json", optional: false, reloadOnChange: true)
              .Build();

            return configurationBuilder["ConnectionString"];
        }
    }
}
