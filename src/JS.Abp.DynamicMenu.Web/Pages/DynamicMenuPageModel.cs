using JS.Abp.DynamicMenu.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.DynamicMenu.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class DynamicMenuPageModel : AbpPageModel
{
    protected DynamicMenuPageModel()
    {
        LocalizationResourceType = typeof(DynamicMenuResource);
        ObjectMapperContext = typeof(DynamicMenuWebModule);
    }
}
