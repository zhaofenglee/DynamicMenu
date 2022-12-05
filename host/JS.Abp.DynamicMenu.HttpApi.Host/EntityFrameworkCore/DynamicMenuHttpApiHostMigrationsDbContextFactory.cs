using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JS.Abp.DynamicMenu.EntityFrameworkCore;

public class DynamicMenuHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DynamicMenuHttpApiHostMigrationsDbContext>
{
    public DynamicMenuHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DynamicMenuHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("DynamicMenu"));

        return new DynamicMenuHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
