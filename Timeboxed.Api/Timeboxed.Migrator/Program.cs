using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Timeboxed.Data;

namespace Timeboxed.Migator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = args.Any() ? args[0] : GetConfig()["TimeboxedConnectionString"];
            using var context = new TimeboxedContextFactory().CreateDbContext(new[] { connectionString });

            Console.WriteLine($"Starting Migration");
            if (context.Database.CanConnect())
            {
                context.Database.Migrate();
                Console.WriteLine("Migration complete");
            }
            else
            {
                Console.WriteLine("Migration failed");
            }
        }

        public static IConfiguration GetConfig() =>
            new ConfigurationBuilder().AddEnvironmentVariables().Build();
    }
}
