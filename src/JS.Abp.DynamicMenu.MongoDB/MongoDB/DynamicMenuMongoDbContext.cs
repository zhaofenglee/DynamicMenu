using JS.Abp.DynamicMenu.MenuItems;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.DynamicMenu.MongoDB;

[ConnectionStringName(DynamicMenuDbProperties.ConnectionStringName)]
public class DynamicMenuMongoDbContext : AbpMongoDbContext, IDynamicMenuMongoDbContext
{
    public IMongoCollection<MenuItem> MenuItems => Collection<MenuItem>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureDynamicMenu();

        modelBuilder.Entity<MenuItem>(b => { b.CollectionName = DynamicMenuDbProperties.DbTablePrefix + "MenuItems"; });
    }
}