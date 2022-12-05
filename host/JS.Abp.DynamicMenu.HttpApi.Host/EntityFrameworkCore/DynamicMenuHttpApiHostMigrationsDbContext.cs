using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.DynamicMenu.EntityFrameworkCore;

public class DynamicMenuHttpApiHostMigrationsDbContext : AbpDbContext<DynamicMenuHttpApiHostMigrationsDbContext>
{
    public DynamicMenuHttpApiHostMigrationsDbContext(DbContextOptions<DynamicMenuHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDynamicMenu();
    }
}
