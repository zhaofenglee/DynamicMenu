using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace JS.Abp.DynamicMenu;

[DependsOn(
    typeof(DynamicMenuDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DynamicMenuApplicationContractsModule : AbpModule
{

}
