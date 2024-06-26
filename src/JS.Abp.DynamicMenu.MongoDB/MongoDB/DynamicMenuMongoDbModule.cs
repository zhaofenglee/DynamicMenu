using JS.Abp.DynamicMenu.MenuItems;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace JS.Abp.DynamicMenu.MongoDB;

[DependsOn(
    typeof(DynamicMenuDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class DynamicMenuMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<DynamicMenuMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<MenuItem, MenuItems.MongoMenuItemRepository>();

        });
    }
}