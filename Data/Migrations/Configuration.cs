namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.AgriConnectContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            // Construct the path to the MDF file directly here
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var databasePath = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\Database\AgriConnectDb.mdf"));

            // Set the connection string
            this.TargetDatabase = new DbConnectionInfo($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databasePath};Integrated Security=True", "System.Data.SqlClient");
        }

        protected override void Seed(Data.AgriConnectContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
