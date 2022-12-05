using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace JS.Abp.DynamicMenu.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class DynamicMenuBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DynamicMenu";
}
