using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using JS.Abp.DynamicMenu.Localization;
using JS.Abp.DynamicMenu.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using JS.Abp.DynamicMenu.Permissions;
using Volo.Abp.Mapperly;

namespace JS.Abp.DynamicMenu.Web;

[DependsOn(
    typeof(DynamicMenuApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpMapperlyModule)
    )]
public class DynamicMenuWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(DynamicMenuResource), typeof(DynamicMenuWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DynamicMenuWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new DynamicMenuMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DynamicMenuWebModule>();
        });
        
        context.Services.AddMapperlyObjectMapper<DynamicMenuWebModule>();

        Configure<RazorPagesOptions>(options =>
        {
                //Configure authorization.
            });
    }
}
