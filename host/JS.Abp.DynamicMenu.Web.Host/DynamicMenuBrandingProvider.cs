using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace JS.Abp.DynamicMenu;

[Dependency(ReplaceServices = true)]
public class DynamicMenuBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DynamicMenu";
}
