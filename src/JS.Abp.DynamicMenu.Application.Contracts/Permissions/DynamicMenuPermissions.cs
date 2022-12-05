using Volo.Abp.Reflection;

namespace JS.Abp.DynamicMenu.Permissions;

public class DynamicMenuPermissions
{
    public const string GroupName = "DynamicMenu";

    public static class MenuItems
    {
        public const string Default = GroupName + ".MenuItems";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DynamicMenuPermissions));
    }
}
