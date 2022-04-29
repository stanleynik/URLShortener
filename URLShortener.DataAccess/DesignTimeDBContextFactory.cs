using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccess
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<URLShortenerContext>
    {
        // This class create the DbContext (URLShortenerContext) and allow to run migrations and update the database in design time
        // To Create Migrations and Create/Update the database from the Package Manager Console use the commands below:
        // dotnet ef migrations add Initial
        // dotnet ef database update
        URLShortenerContext IDesignTimeDbContextFactory<URLShortenerContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<URLShortenerContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);
          

            return new URLShortenerContext(builder.Options);
        }
    }
}
