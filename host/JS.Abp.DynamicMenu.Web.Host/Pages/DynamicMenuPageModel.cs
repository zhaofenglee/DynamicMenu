using JS.Abp.DynamicMenu.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.DynamicMenu.Pages;

public abstract class DynamicMenuPageModel : AbpPageModel
{
    protected DynamicMenuPageModel()
    {
        LocalizationResourceType = typeof(DynamicMenuResource);
    }
}
