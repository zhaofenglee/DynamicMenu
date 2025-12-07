using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Caching;
using Volo.Abp.Mapperly;

namespace JS.Abp.DynamicMenu;

[DependsOn(
    typeof(DynamicMenuDomainModule),
    typeof(DynamicMenuApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule),
    typeof(AbpCachingModule)
    )]
public class DynamicMenuApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<DynamicMenuApplicationModule>();
    }
}
