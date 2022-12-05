using JS.Abp.DynamicMenu.Localization;
using Volo.Abp.Application.Services;

namespace JS.Abp.DynamicMenu;

public abstract class DynamicMenuAppService : ApplicationService
{
    protected DynamicMenuAppService()
    {
        LocalizationResource = typeof(DynamicMenuResource);
        ObjectMapperContext = typeof(DynamicMenuApplicationModule);
    }
}
