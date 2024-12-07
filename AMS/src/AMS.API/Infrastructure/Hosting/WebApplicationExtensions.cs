using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AMS.API.Infrastructure.Hosting
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MigrateDbContext<TContext>(this WebApplication webApplication, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();

                try
                {
                    Log.Information("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    InvokeSeeder(seeder, context, services);

                    Log.Information("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                }
            }

            return webApplication;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}