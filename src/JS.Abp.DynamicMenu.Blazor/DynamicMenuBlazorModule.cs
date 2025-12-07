using Microsoft.Extensions.DependencyInjection;
using JS.Abp.DynamicMenu.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Http.Client;
using Volo.Abp.Mapperly;

namespace JS.Abp.DynamicMenu.Blazor;

[DependsOn(
    typeof(DynamicMenuApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpMapperlyModule),
    typeof(AbpHttpClientModule)
    )]
public class DynamicMenuBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        
        context.Services.AddMapperlyObjectMapper<DynamicMenuBlazorModule>();

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DynamicMenuMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(DynamicMenuBlazorModule).Assembly);
        });
    }
}
