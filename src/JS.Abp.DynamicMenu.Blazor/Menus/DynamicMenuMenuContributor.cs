using JS.Abp.DynamicMenu.Localization;
using JS.Abp.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.Permissions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DynamicMenu.Blazor.Menus;

public class DynamicMenuMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
            await AddDynamicMenuManagementMenuItemAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<DynamicMenuResource>();
        var administration = context.Menu.GetAdministration();
        administration.AddItem(
           new ApplicationMenuItem(
               DynamicMenuMenus.MenuItems,
               l["Menu:MenuItems"],
               url: "/menu-items",
               icon: "fab fa-elementor",
               requiredPermissionName: DynamicMenuPermissions.MenuItems.Default)
       );
        //Add main menu items.
        //context.Menu.AddItem(new ApplicationMenuItem(DynamicMenuMenus.Prefix, displayName: "DynamicMenu", "/DynamicMenu", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }

    protected virtual async Task AddDynamicMenuManagementMenuItemAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<DynamicMenuResource>();

        var menuAppService = context.ServiceProvider.GetRequiredService<IMenuItemsAppService>();

        var menuItems = await menuAppService.GetListAsync();

        if (menuItems.Items.Count > 0)
        {
            foreach (var menuItemDto in menuItems.Items.Where(x => x.ParentId == null && x.IsActive))
            {
                AddChildItems(menuItemDto, menuItems.Items.ToList(), l, context.Menu);
            }
        }
    }

    private void AddChildItems(MenuItemDto menuItem, List<MenuItemDto> source, IStringLocalizer localizer, IHasMenuItems parent = null)
    {
        menuItem.DisplayName = localizer[menuItem.DisplayName];
        var applicationMenuItem = CreateApplicationMenuItem(menuItem);

        foreach (var item in source.Where(x => x.ParentId == menuItem.Id && x.IsActive))
        {
            AddChildItems(item, source, localizer, applicationMenuItem);
        }

        parent?.Items.Add(applicationMenuItem);
    }
    private ApplicationMenuItem CreateApplicationMenuItem(MenuItemDto menuItem)
    {
        if (string.IsNullOrWhiteSpace(menuItem.Permission))
        {
            return new ApplicationMenuItem(
            menuItem.Name,
            menuItem.DisplayName,
            menuItem.Url,
            menuItem.Icon,
            menuItem.Order,
            customData: null,
            menuItem.Target,
            menuItem.ElementId,
            menuItem.CssClass
        );
        }
        else
        {
            return new ApplicationMenuItem(
           menuItem.Name,
           menuItem.DisplayName,
           menuItem.Url,
           menuItem.Icon,
           menuItem.Order,
           customData: null,
           menuItem.Target,
           menuItem.ElementId,
           menuItem.CssClass,
           requiredPermissionName: menuItem.Permission
       );
        }

    }


}
