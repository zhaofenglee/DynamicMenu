using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace JS.Abp.DynamicMenu;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DynamicMenuHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DynamicMenuConsoleApiClientModule : AbpModule
{

}
