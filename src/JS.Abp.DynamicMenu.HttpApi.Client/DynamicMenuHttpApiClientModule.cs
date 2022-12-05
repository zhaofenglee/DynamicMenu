using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace JS.Abp.DynamicMenu;

[DependsOn(
    typeof(DynamicMenuApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DynamicMenuHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DynamicMenuApplicationContractsModule).Assembly,
            DynamicMenuRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DynamicMenuHttpApiClientModule>();
        });

    }
}
