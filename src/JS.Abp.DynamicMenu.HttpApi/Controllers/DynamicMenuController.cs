using JS.Abp.DynamicMenu.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace JS.Abp.DynamicMenu.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class DynamicMenuController : AbpControllerBase
{
    protected DynamicMenuController()
    {
        LocalizationResource = typeof(DynamicMenuResource);
    }
}
