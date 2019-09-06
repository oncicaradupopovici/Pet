using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Subtext.Scripting;

namespace Pet.Migrations
{
    public class DatabaseMigrator
    {
        private readonly string _connectionString;

        public DatabaseMigrator(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task MigrateToLatestVersion()
        {
            Console.WriteLine("Migrating Database");
            using (var cnx = new SqlConnection(_connectionString))
            {
                await cnx.OpenAsync();

                await MigrateTables(cnx);
                Console.WriteLine("-----------------------------------------------");
                await MigrateViews(cnx);
                Console.WriteLine("-----------------------------------------------");
            }

            Console.WriteLine("Database Migration completed");
        }

        private async Task MigrateTables(SqlConnection cnx)
        {
            Console.WriteLine("Migrating tables");
            foreach (var tableName in GetTableNames())
            {
                Console.WriteLine($" * {tableName}");
                await ExecuteNonQuery(cnx, GetTableContent(tableName));
            }
        }

        private async Task MigrateViews(SqlConnection cnx)
        {
            Console.WriteLine("Migrating views");
            foreach (var viewName in GetViewNames())
            {
                Console.WriteLine($" * {viewName}");
                await ExecuteNonQuery(cnx, $"if exists (select 1 from sys.objects where name = '{viewName}') drop view {viewName}");
                await ExecuteNonQuery(cnx, GetViewContent(viewName));
            }
        }

        private string[] GetTableNames()
        {
            var all = typeof(DatabaseMigrator).GetTypeInfo().Assembly.GetManifestResourceNames();
            var tables = all
                .Where(x => x.StartsWith("Pet.Migrations.SqlScripts.Tables.") && x.EndsWith(".sql"))
                .Select(x => x.Replace("Pet.Migrations.SqlScripts.Tables.", string.Empty).Replace(".sql", string.Empty))
                .OrderBy(x => x)
                .Select(x => x.Substring(x.IndexOf('.') + 1))
                .ToArray();

            return tables;
        }

        private string[] GetViewNames()
        {
            var all = typeof(DatabaseMigrator).GetTypeInfo().Assembly.GetManifestResourceNames();
            var tables = all
                .Where(x => x.StartsWith("Pet.Migrations.SqlScripts.Views.") && x.EndsWith(".sql"))
                .Select(x => x.Replace("Pet.Migrations.SqlScripts.Views.", string.Empty).Replace(".sql", string.Empty))
                .OrderBy(x => x)
                .Select(x => x.Substring(x.IndexOf('.') + 1))
                .ToArray();

            return tables;
        }

        public string GetTableContent(string name)
        {
            var manifestResourceNames = typeof(DatabaseMigrator).GetTypeInfo().Assembly.GetManifestResourceNames()
                .First(x => x.StartsWith("Pet.Migrations.SqlScripts.Tables.") && x.EndsWith($"{name}.sql"));
            using (var stream = typeof(DatabaseMigrator).GetTypeInfo().Assembly.GetManifestResourceStream(manifestResourceNames))
            {
                if (stream == null)
                {
                    throw new Exception($"Embedded resource, {name}, not found.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string GetViewContent(string name)
        {
            var manifestResourceNames = typeof(DatabaseMigrator).GetTypeInfo().Assembly.GetManifestResourceNames()
                .First(x => x.StartsWith("Pet.Migrations.SqlScripts.Views.") && x.EndsWith($"{name}.sql"));
            using (var stream = typeof(DatabaseMigrator).GetTypeInfo().Assembly.GetManifestResourceStream(manifestResourceNames))
            {
                if (stream == null)
                {
                    throw new Exception($"Embedded resource, {name}, not found.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static Task ExecuteNonQuery(SqlConnection cnx, string sql)
        {
            var runner = new SqlScriptRunner(sql);
            using (var transaction = cnx.BeginTransaction())
            {
                runner.Execute(transaction);
                transaction.Commit();
            }

            return Task.CompletedTask;
        }
    }
}
