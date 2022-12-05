using JS.Abp.DynamicMenu.Localization;
using Volo.Abp.AspNetCore.Components;

namespace JS.Abp.DynamicMenu.Blazor.Server.Host;

public abstract class DynamicMenuComponentBase : AbpComponentBase
{
    protected DynamicMenuComponentBase()
    {
        LocalizationResource = typeof(DynamicMenuResource);
    }
}
