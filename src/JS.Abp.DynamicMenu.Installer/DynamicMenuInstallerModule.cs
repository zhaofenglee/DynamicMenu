using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace JS.Abp.DynamicMenu;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DynamicMenuInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DynamicMenuInstallerModule>();
        });
    }
}
