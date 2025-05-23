﻿using JS.Abp.DynamicMenu.Localization;
using JS.Abp.DynamicMenu.MenuItems;
using JS.Abp.DynamicMenu.Permissions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NUglify.Helpers;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.DynamicMenu.Blazor.Menus;

public class DynamicMenuMenuContributor : IMenuContributor
{
    public DynamicMenuMenuContributor()
    {
        
    }
    
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
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
               url: "/DynamicMenu/MenuItems",
               icon: "fab fa-elementor",
               requiredPermissionName: DynamicMenuPermissions.MenuItems.Default)
       );
        //Add main menu items.
        //context.Menu.AddItem(new ApplicationMenuItem(DynamicMenuMenus.Prefix, displayName: "DynamicMenu", "/DynamicMenu", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }

}
