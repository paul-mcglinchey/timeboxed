using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace Timeboxed.Data
{
    public class TimeboxedContextFactory : IDesignTimeDbContextFactory<TimeboxedContext>
    {
        public TimeboxedContext CreateDbContext(string[] args)
        {
            var connectionString = args.Any() ? args[0] : Environment.GetEnvironmentVariable("TimeboxedConnectionString") ?? "server=localhost;initial catalog=Timeboxed;integrated security=true";
            var optionsBuilder = new DbContextOptionsBuilder<TimeboxedContext>();
            optionsBuilder
                .UseSqlServer(
                    connectionString, 
                    options => 
                    { 
                        options.MigrationsAssembly("Timeboxed.Data"); 
                        options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        options.EnableRetryOnFailure();
                    })
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                       LogLevel.Information);

            return new TimeboxedContext(optionsBuilder.Options);
        }
    }
}
