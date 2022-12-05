using Volo.Abp.Modularity;

namespace JS.Abp.DynamicMenu;

[DependsOn(
    typeof(DynamicMenuApplicationModule),
    typeof(DynamicMenuDomainTestModule)
    )]
public class DynamicMenuApplicationTestModule : AbpModule
{

}
