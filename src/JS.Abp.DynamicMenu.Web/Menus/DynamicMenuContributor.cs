using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JS.Abp.DynamicMenu.Localization;
using JS.Abp.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.Permissions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DynamicMenu.Web.Menus;

public class DynamicMenuContributor : IMenuContributor
{
   
    public DynamicMenuContributor()
    {
        
    }
    
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await AddDynamicMenuManagementMenuItemAsync(context);
        }
    }
    
    protected virtual async Task AddDynamicMenuManagementMenuItemAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<DynamicMenuResource>();
        var menuAppService = context.ServiceProvider.GetRequiredService<IMenuItemsAppService>();

        var menuItems = await menuAppService.GetListAsync();
        
        if (menuItems.Items.Count > 0)
        {
            foreach (var menuItemDto in menuItems.Items.Where(x => x.ParentId == null && x.IsActive).OrderBy(x => x.Order)
                         .ThenBy(x => x.Name))
            {
               
                var menuItem = context.Menu.FindMenuItem(menuItemDto.Name);
                if (menuItem==null)
                {
                    AddChildItems(menuItemDto, menuItems.Items.ToList(), l, context.Menu);
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(menuItemDto.Url))
                    {
                        menuItem.Url = menuItemDto.Url;
                    }
                    if (!string.IsNullOrWhiteSpace(menuItemDto.Icon))
                    {
                        menuItem.Icon = menuItemDto.Icon;
                    }
                    menuItem.Order = menuItemDto.Order;
                }
            }
        }
       
    }

    private void AddChildItems(MenuItemDto menuItem, List<MenuItemDto> source, IStringLocalizer localizer, IHasMenuItems parent = null)
    {
        menuItem.DisplayName = localizer[menuItem.DisplayName];
        var applicationMenuItem = CreateApplicationMenuItem(menuItem);

        foreach (var item in source.Where(x => x.ParentId == menuItem.Id && x.IsActive).OrderBy(x => x.Order)
                     .ThenBy(x => x.Name))
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
           menuItem.Target,
           menuItem.ElementId,
           menuItem.CssClass,
           requiredPermissionName: menuItem.Permission
       );
        }

    }

}
