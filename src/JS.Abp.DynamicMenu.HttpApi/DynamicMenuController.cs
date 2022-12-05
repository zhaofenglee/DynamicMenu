using JS.Abp.DynamicMenu.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace JS.Abp.DynamicMenu;

public abstract class DynamicMenuController : AbpControllerBase
{
    protected DynamicMenuController()
    {
        LocalizationResource = typeof(DynamicMenuResource);
    }
}
