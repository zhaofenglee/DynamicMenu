using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.DynamicMenu.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(DynamicMenuBlazorModule)
    )]
public class DynamicMenuBlazorServerModule : AbpModule
{

}
