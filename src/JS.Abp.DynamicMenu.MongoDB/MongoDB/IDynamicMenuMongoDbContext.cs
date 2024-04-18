using JS.Abp.DynamicMenu.MenuItems;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.DynamicMenu.MongoDB;

[ConnectionStringName(DynamicMenuDbProperties.ConnectionStringName)]
public interface IDynamicMenuMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<MenuItem> MenuItems { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}