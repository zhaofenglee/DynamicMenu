using JS.Abp.DynamicMenu.Localization;
using Volo.Abp.AspNetCore.Components;

namespace JS.Abp.DynamicMenu.Blazor;

public abstract class DynamicMenuComponentBase : AbpComponentBase
{
    protected DynamicMenuComponentBase()
    {
        LocalizationResource = typeof(DynamicMenuResource);
    }
}
