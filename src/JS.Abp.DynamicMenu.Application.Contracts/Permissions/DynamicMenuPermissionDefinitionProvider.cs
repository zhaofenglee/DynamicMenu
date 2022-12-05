using JS.Abp.DynamicMenu.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace JS.Abp.DynamicMenu.Permissions;

public class DynamicMenuPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DynamicMenuPermissions.GroupName, L("Permission:DynamicMenu"));

        var menuItemPermission = myGroup.AddPermission(DynamicMenuPermissions.MenuItems.Default, L("Permission:MenuItems"));
        menuItemPermission.AddChild(DynamicMenuPermissions.MenuItems.Create, L("Permission:Create"));
        menuItemPermission.AddChild(DynamicMenuPermissions.MenuItems.Edit, L("Permission:Edit"));
        menuItemPermission.AddChild(DynamicMenuPermissions.MenuItems.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DynamicMenuResource>(name);
    }
}
