using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Pet.Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets(Assembly.GetCallingAssembly())
                .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString("Pet_Database");
            //var connectionString = configuration.GetConnectionString("Tenant_d646cb7a-a064-4c9d-b241-633b3baf570e");
            var migrator = new DatabaseMigrator(connectionString);
            migrator.MigrateToLatestVersion().GetAwaiter().GetResult();

            Console.ReadKey();
        }
    }
}
