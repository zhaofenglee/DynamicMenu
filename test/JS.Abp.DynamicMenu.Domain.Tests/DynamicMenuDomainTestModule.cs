using Volo.Abp.Modularity;

namespace JS.Abp.DynamicMenu;

[DependsOn(
    typeof(DynamicMenuDomainModule),
    typeof(DynamicMenuTestBaseModule)
)]
public class DynamicMenuDomainTestModule : AbpModule
{

}
