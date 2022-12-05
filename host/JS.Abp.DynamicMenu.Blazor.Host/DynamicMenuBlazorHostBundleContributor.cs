using Volo.Abp.Bundling;

namespace JS.Abp.DynamicMenu.Blazor.Host;

public class DynamicMenuBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
