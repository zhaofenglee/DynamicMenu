using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DynamicMenu.Web.Menus;

public class DynamicMenuMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(DynamicMenuMenus.Prefix, displayName: "DynamicMenu", "~/DynamicMenu", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
